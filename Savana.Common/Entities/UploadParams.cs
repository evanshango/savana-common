namespace Savana.Common.Entities
{
    public class UploadParams
    {
        public string FolderName { get; set; }
        public string S3Endpoint { get; set; }
        public string S3SecretKey { get; set; }
        public string S3AccessKey { get; set; }
        public string S3BucketName { get; set; }
        public int FileWidth { get; set; }
        public int FileHeight { get; set; }

        public UploadParams(string folderName, string s3Endpoint, string s3SecretKey, string s3AccessKey,
            string s3BucketName, int fileWidth, int fileHeight)
        {
            FolderName = folderName;
            S3Endpoint = s3Endpoint;
            S3SecretKey = s3SecretKey;
            S3AccessKey = s3AccessKey;
            S3BucketName = s3BucketName;
            FileWidth = fileWidth;
            FileHeight = fileHeight;
        }
    }
}