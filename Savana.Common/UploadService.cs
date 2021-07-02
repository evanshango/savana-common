using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.S3;
using Amazon.S3.Model;
using Microsoft.AspNetCore.Http;
using Savana.Common.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Savana.Common
{
    public class UploadService : IUploadService
    {
        private readonly string _endpoint;
        private readonly string _accessKey;
        private readonly string _secretKey;
        private readonly string _bucketName;
        private readonly string _folderName;

        public UploadService(string endpoint, string accessKey, string secretKey, string bucketName, string folderName)
        {
            _endpoint = endpoint;
            _accessKey = accessKey;
            _secretKey = secretKey;
            _bucketName = bucketName;
            _folderName = folderName;
        }

        private static string ResizedImage(IImageInfo image, int? maxWidth, int? maxHeight)
        {
            string dimensionString;

            if (maxWidth != null && maxHeight != null)
            {
                if (image.Width <= maxWidth && image.Height <= maxHeight)
                {
                    dimensionString = $"{image.Height.ToString()},{image.Width.ToString()}";
                }
                else
                {
                    var widthRatio = (double) image.Width / maxWidth;
                    var heightRatio = (double) image.Height / maxHeight;
                    var ratio = Math.Max((double) widthRatio, (double) heightRatio);
                    
                    var newWidth = (int) (image.Width / ratio);
                    var newHeight = (int) (image.Height / ratio);
                    
                    dimensionString = $"{newHeight.ToString()},{newWidth.ToString()}";
                }
            }
            else
            {
                dimensionString = $"{image.Height.ToString()},{image.Width.ToString()}";
            }

            return dimensionString;
        }

        public async Task<string> UploadFile(IFormFile file, int? width, int? height)
        {
            var extension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
            var fileName = $"{DateTime.Now:yyyyMMddHHmmssffff}.{extension.ToLower()}";

            var clientConfig = new AmazonS3Config {ServiceURL = $"https://{_endpoint}"};
            var s3Client = new AmazonS3Client(_accessKey, _secretKey, clientConfig);

            try
            {
                using var image = await Image.LoadAsync(file.OpenReadStream());
                var newSize = ResizedImage(image, width, height);
                var sizeArray = newSize.Split(",");
                image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));

                await using var stream = new MemoryStream();
                await image.SaveAsPngAsync(stream);

                var request = new PutObjectRequest
                {
                    BucketName = $"{_bucketName}/{_folderName}",
                    Key = fileName,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = stream
                };

                var response = await s3Client.PutObjectAsync(request);
                return response.HttpStatusCode == HttpStatusCode.OK ? $"{_folderName}/{fileName}" : "default";
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occured while uploading file... {ex.Message}");
                return "default";
            }
        }
    }
}