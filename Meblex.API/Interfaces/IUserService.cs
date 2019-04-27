using System.Threading.Tasks;
using Meblex.API.Models;

namespace Meblex.API.Interfaces
{
    public interface IUserService
    {
        Task<Client> GetUserData(string login);
        Task<bool> CheckIfPasswordIsMatching(int id, string password);
        Task<bool> CheckIfEmailIsMatching(int id, string email);
        Task<bool> CheckIfUserWithEmailExist(string email);
        Task<bool> UpdateUserEmail(int id, string email);
        Task<bool> UpdateUserPassword(int id, string password);
    }
}