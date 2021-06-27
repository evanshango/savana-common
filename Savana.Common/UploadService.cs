using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Savana.Common.Entities;
using Savana.Common.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Savana.Common
{
    public class UploadService : IUploadService
    {
        private readonly ILogger<UploadService> _logger;

        public UploadService(ILogger<UploadService> logger)
        {
            _logger = logger;
        }

        public async Task<string> UploadFile(IFormFile file, UploadParams @params)
        {
            var extension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
            var fileName = $"{DateTime.Now:yyyyMMddHHmmssffff}.{extension.ToLower()}";

            var clientConfig = new AmazonS3Config {ServiceURL = $"https://{@params.S3Endpoint}"};
            var s3Client = new AmazonS3Client(@params.S3AccessKey, @params.S3SecretKey, clientConfig);

            try
            {
                using var image = await Image.LoadAsync(file.OpenReadStream());
                var newSize = ResizedImage(image, @params.FileWidth, @params.FileHeight);
                var sizeArray = newSize.Split(",");
                image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));

                await using var stream = new MemoryStream();
                await image.SaveAsPngAsync(stream);

                var request = new PutObjectRequest
                {
                    BucketName = $"{@params.S3BucketName}/{@params.FolderName}",
                    Key = fileName,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = stream
                };

                var response = await s3Client.PutObjectAsync(request);
                return response.HttpStatusCode == HttpStatusCode.OK ? $"{@params.FolderName}/{fileName}" : "default";
            }
            catch (Exception ex)
            {
                _logger.LogInformation("An unknown error occured while uploading file", ex);
                return "default";
            }
        }

        private static string ResizedImage(IImageInfo image, int maxWidth, int maxHeight)
        {
            if (image.Width <= maxWidth && image.Height <= maxHeight)
                return $"{image.Height.ToString()},{image.Width.ToString()}";

            var widthRatio = (double) image.Width / maxWidth;
            var heightRatio = (double) image.Height / maxHeight;
            var ratio = Math.Max(widthRatio, heightRatio);

            var newWidth = (int) (image.Width / ratio);
            var newHeight = (int) (image.Height / ratio);

            return $"{newHeight.ToString()},{newWidth.ToString()}";
        }
    }
}