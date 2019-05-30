using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Amazon;
using Amazon.Runtime;
using Amazon.Runtime.Internal;
using Amazon.S3;
using Amazon.S3.Model;
using Amazon.S3.Transfer;
using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Http;

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

        public async Task SetPolicy()
        {
            var policy =
                "{\r\n  \"Id\": \"Policy1559239182609\",\r\n  \"Version\": \"2012-10-17\",\r\n  \"Statement\": [\r\n    {\r\n      \"Sid\": \"Stmt1559239176582\",\r\n      \"Action\": [\r\n        \"s3:GetObject\"\r\n      ],\r\n      \"Effect\": \"Allow\",\r\n      \"Resource\": \"arn:aws:s3:::meblex-wip-cdn\",\r\n      \"Principal\": \"*\"\r\n    }\r\n  ]\r\n}";
            var policyRequest = new PutBucketPolicyRequest(){BucketName = BucketName, Policy =policy};
            await _client.PutBucketPolicyAsync(policyRequest);

        }

        public async Task<List<string>> SafePhotos(List<IFormFile> photos)
        {
            var photosName = new List<string>();
            foreach (var photo in photos)
            {
                var photoName = await SafePhoto(photo);
                photosName.Add(photoName);
            }

            return photosName;
        }

        public async Task<string> SafePhoto(IFormFile photo)
        {
            var fileTransferUtility = new TransferUtility(_client);
            var photoName = GetHashedName(photo);
            await fileTransferUtility.UploadAsync(photo.OpenReadStream(), BucketName, photoName);
            await SetPolicy();
            return photoName;
        }

        private string GetHashedName(IFormFile photo)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(photo.OpenReadStream());
            var photoName = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant() + Path.GetExtension(photo.FileName);
            return photoName;
        }
    }
}
