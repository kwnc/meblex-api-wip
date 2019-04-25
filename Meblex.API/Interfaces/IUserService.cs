using System.Threading.Tasks;
using Meblex.API.Models;

namespace Meblex.API.Interfaces
{
    public interface IUserService
    {
        Task<Client> GetUserData(string login);
    }
}