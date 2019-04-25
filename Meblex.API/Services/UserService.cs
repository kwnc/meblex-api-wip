using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dawn;
using Meblex.API.Context;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Meblex.API.Services
{
    public class UserService:IUserService
    {
        private readonly MeblexDbContext _context;
        private readonly IMapper _mapper;
        public UserService(MeblexDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;

        }
        public async Task<Client> GetUserData(string login)
        {
            var Login = Guard.Argument(login, nameof(login)).NotNull().NotEmpty().NotWhiteSpace();

            var user = await _context.Clients.FirstOrDefaultAsync(s => s.User.Email == Login);

            return user;

        }
    }
}
