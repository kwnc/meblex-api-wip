using System.Threading.Tasks;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Response;
using Meblex.API.Models;

namespace Meblex.API.Interfaces
{
    public interface IClientService
    {
        Task<int> GetClientIdFromUserId(int userId);
        Task<bool> UpdateClientData(ClientUpdateDto client, int clientId);
        Task<ClientUpdateResponse> GetClientData(int clientId);
    }
}