using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Meblex.API.Interfaces
{
    public interface IPhotoService
    {
        Task<List<string>> SafePhotos(List<IFormFile> photos);
        Task<string> SafePhoto(IFormFile photo);
    }
}