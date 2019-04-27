using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Meblex.API.Context;
using Meblex.API.FormsDto.Response;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.EntityFrameworkCore;

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

        public async Task<bool> UpdateClientData(Client client)
        {
            var Client = Guard.Argument(client, nameof(client)).NotNull().Value;


            var clientDb = await _context.Clients.SingleOrDefaultAsync(x => x.ClientId == Client.ClientId);

            clientDb = _mapper.Map<Client, Client>(Client, clientDb);

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
