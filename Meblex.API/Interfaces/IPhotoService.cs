using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace Meblex.API.Interfaces
{
    public interface IPhotoService
    {
        List<string> SafePhotos(List<IFormFile> photos);
    }
}