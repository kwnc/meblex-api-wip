using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Meblex.API.Helper
{
    public class IFormFilePhoto : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var photos = new List<IFormFile>();
            if (value == null) return new ValidationResult("No file/s uploaded");
            if (IsList(value))
            {
                foreach (var photo in (List<IFormFile>)value)
                {
                    photos.Add(photo);
                }
            }
            else
            {
                photos.Add((IFormFile) value);
            }

            foreach (var photo in photos)
            {
                if(!IsImage(photo.OpenReadStream())) return new ValidationResult("Not valid photo/photos");
            }
            

            return ValidationResult.Success;
        }

        private bool IsList(object o)
        {
            if (o == null) return false;
            return o is IList &&
                   o.GetType().IsGenericType &&
                   o.GetType().GetGenericTypeDefinition().IsAssignableFrom(typeof(List<>));
        }

        private bool IsImage(Stream stream)
        {
            try
            {
                using (var image = Image.FromStream(stream)) { }

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
