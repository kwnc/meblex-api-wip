using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Meblex.API.Context;
using Meblex.API.DTO;
using Meblex.API.FormsDto.Response;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.EntityFrameworkCore;
using Mapper = AgileObjects.AgileMapper.Mapper;

namespace Meblex.API.Services
{
    public class ClientService:IClientService
    {
        private readonly IMapper _mapper;
        private readonly MeblexDbContext _context;
        public ClientService(IMapper mapper, MeblexDbContext context)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<bool> UpdateClientData(ClientUpdateDto client, int clientId)
        {
            var Client = Guard.Argument(client, nameof(client)).NotNull().Value;
            var ClientId = Guard.Argument(clientId, nameof(clientId)).NotNegative();


            var clientDb = await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == ClientId);


//            Mapper.Map(Client).Over(clientDb, cfg => 
//                cfg.IgnoreTargetMembersWhere(t => 
//                    t.IsPropertyMatching(info => 
//                        info.GetType().GetProperties().All(p =>
//                            p.GetValue(info) != null))));


            clientDb.Name = !string.IsNullOrEmpty(Client.Name) ? Client.Name : clientDb.Name; 
            clientDb.Address = !string.IsNullOrEmpty(Client.Address) ? Client.Address : clientDb.Address;
            clientDb.City = !string.IsNullOrEmpty(Client.City) ? Client.City : clientDb.City;
            clientDb.NIP = !string.IsNullOrEmpty(Client.NIP) ? int.Parse(Client.NIP) : clientDb.NIP;
            clientDb.PostCode = !string.IsNullOrEmpty(Client.PostCode) ? int.Parse(Client.PostCode) : clientDb.PostCode;
            clientDb.State = !string.IsNullOrEmpty(Client.State) ? Client.State : clientDb.State;

            _context.Clients.Update(clientDb);

            var isSaved = await _context.SaveChangesAsync();

            return isSaved != 0;

        }

        public async Task<int> GetClientIdFromUserId(int userId)
        {
            var UserId = Guard.Argument(userId, nameof(userId)).NotNegative();

            var client = await _context.Clients.SingleOrDefaultAsync(x => x.User.UserId == UserId);

            return client.ClientId;
        }

        public async Task<ClientUpdateResponse> GetClientData(int clientId)
        {
            var ClientId = Guard.Argument(clientId, nameof(clientId)).NotNegative();

            var client = await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == ClientId);

            var clientDto = _mapper.Map<ClientUpdateResponse>(client);

            return clientDto;
        }
    }
}
