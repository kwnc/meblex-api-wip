using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Meblex.API.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Meblex.API.Services
{
    public class PhotoService:IPhotoService
    {
        private readonly string Images = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");

        public List<string> SafePhotos(List<IFormFile> photos)
        {
            var photosName = new List<string>();
            var md5 = MD5.Create();
            foreach (var photo in photos)
            {
                var hash = md5.ComputeHash(photo.OpenReadStream());
                var photoName = BitConverter.ToString(hash).Replace("-", string.Empty).ToLowerInvariant()+ Path.GetExtension(photo.FileName);
                photosName.Add(photoName);
            }


            for (int i = 0; i < photos.Count; i++)
            {
                using (var fileStream = new FileStream(Path.Combine(Images, photosName[i]), FileMode.Create))
                {
                    photos[i].CopyToAsync(fileStream);
                }
            }

            return photosName;
        }
    }
}
