using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Amazon.Runtime;
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

        /// <summary>
        /// Creates an instance of an upload service
        /// </summary>
        /// <param name="endpoint"></param>
        /// <param name="accessKey"></param>
        /// <param name="secretKey"></param>
        /// <param name="bucketName"></param>
        /// <param name="folderName"></param>
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

        /// <summary>
        /// Returns a path to an uploaded file with the given dimensions
        /// </summary>
        /// <param name="file"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public async Task<string> UploadFile(IFormFile file, int? width, int? height)
        {
            var extension = file.FileName.Split(".")[file.FileName.Split(".").Length - 1];
            var fileName = $"{DateTime.Now:yyyyMMddHHmmssffff}.{extension.ToLower()}";

            var clientConfig = new AmazonS3Config
            {
                ServiceURL = $"https://{_endpoint}",
                Timeout = TimeSpan.FromMinutes(10),
                RetryMode = RequestRetryMode.Standard,
                MaxErrorRetry = 5
            };
            var s3Client = new AmazonS3Client(_accessKey, _secretKey, clientConfig);

            var fileToUpload = file.ContentType.StartsWith("image")
                ? await ImageResizedResult(file, width, height)
                : file.OpenReadStream();

            try
            {
                var request = new PutObjectRequest
                {
                    BucketName = $"{_bucketName}/{_folderName}",
                    Key = fileName,
                    ContentType = file.ContentType,
                    CannedACL = S3CannedACL.PublicRead,
                    InputStream = fileToUpload
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

        private static async Task<Stream> ImageResizedResult(IFormFile file, int? width, int? height)
        {
            using var image = await Image.LoadAsync(file.OpenReadStream());
            var newSize = ResizedImage(image, width, height);
            var sizeArray = newSize.Split(",");
            image.Mutate(x => x.Resize(Convert.ToInt32(sizeArray[1]), Convert.ToInt32(sizeArray[0])));

            await using var stream = new MemoryStream();
            await image.SaveAsPngAsync(stream);
            return stream;
        }

        /// <summary>
        /// Removes the given file name from the storage bucket
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns><value></value></returns>
        public async Task<string> RemoveFile(string fileName)
        {
            var clientConfig = new AmazonS3Config {ServiceURL = $"https://{_endpoint}"};
            var s3Client = new AmazonS3Client(_accessKey, _secretKey, clientConfig);
            var fileToRemove = fileName.Split("/")[1];

            try
            {
                var request = new DeleteObjectRequest
                {
                    BucketName = $"{_bucketName}/{_folderName}",
                    Key = fileToRemove
                };

                var response = await s3Client.DeleteObjectAsync(request);
                return response.HttpStatusCode == HttpStatusCode.OK ? "file removed" : "unable to remove file";
            }
            catch (Exception e)
            {
                Console.WriteLine($"An error occured while removing file from storage... {e.Message}");
                return "unable to remove file";
            }
        }
    }
}