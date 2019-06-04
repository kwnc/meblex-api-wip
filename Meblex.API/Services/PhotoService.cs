using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Transfer;
using Meblex.API.Interfaces;

namespace Meblex.API.Services
{
    public class PhotoService:IPhotoService
    {
        private readonly string Images = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
        private IAmazonS3 _client;
        private AWSCredentials _credentials;
        private AmazonS3Config _config;
        private string BucketName = Environment.GetEnvironmentVariable("SPACES_NAME");
        public PhotoService()
        {
            _config = new AmazonS3Config();
            _config.ServiceURL = Environment.GetEnvironmentVariable("SPACES_REGION");
            _credentials =
                new BasicAWSCredentials(Environment.GetEnvironmentVariable("SPACES_ACCESS_KEY"),
                    Environment.GetEnvironmentVariable("SPACES_SECRET_KEY"));
            _client = new AmazonS3Client(_credentials,_config);

        }

        public async Task<List<string>> SafePhotos(List<string> photos)
        {
            
            var photosName = new List<string>();
            foreach (var photo in photos)
            {
                var photoName = await SafePhoto(photo);
                photosName.Add(photoName);
            }

            return photosName;
        }

        public async Task<string> SafePhoto(string photo)
        {
            var bytes = Convert.FromBase64String(photo.Split(',')[1]);
            var photoName = GetHashedName(bytes) + "." + Regex.Match(photo, @"data:.*?/(?<ext>.*?);base64").Groups["ext"].Value;
            using (var stream = new MemoryStream(bytes))
            {
                using (var fileTransferUtility = new TransferUtility(_client))
                {
                    var awsRequest = new TransferUtilityUploadRequest()
                    {
                        BucketName = BucketName,
                        Key = photoName,
                        InputStream = stream
                    };
                    awsRequest.CannedACL = "public-read";
                    await fileTransferUtility.UploadAsync(awsRequest);

                }
            }

            return photoName;
        }

        private string GetHashedName(Byte[] photo)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(photo);
            var photoName = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant();
            return photoName;
        }
    }
}
