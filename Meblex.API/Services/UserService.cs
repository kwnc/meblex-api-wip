using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        private readonly IAuthService _authService;
        public UserService(MeblexDbContext context, IMapper mapper, IAuthService authService)
        {
            _context = context;
            _mapper = mapper;
            _authService = authService;

        }
        public async Task<Client> GetUserData(string login)
        {
            var Login = Guard.Argument(login, nameof(login)).NotNull().NotEmpty().NotWhiteSpace();

            var user = await _context.Clients.FirstOrDefaultAsync(s => s.User.Email == Login);

            return user;

        }

        public async Task<bool> CheckIfPasswordIsMatching(int id, string password)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();

            var hashedPassword = _authService.PasswordHasher(Password);

            var match = await _context.Users.SingleOrDefaultAsync(x => x.UserId == Id && x.Password == hashedPassword);

            if (match == null)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> CheckIfEmailIsMatching(int id, string email)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();
            var Email = Guard.Argument(email, nameof(email)).NotEmpty().NotNull().NotWhiteSpace();

            var match = await _context.Users.SingleOrDefaultAsync(x => x.UserId == Id && x.Email == Email);

            if (match == null)
            {
                return false;
            }

            return true;

        }

        public async Task<bool> UpdateUserEmail(int id, string email)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();
            var Email = Guard.Argument(email, nameof(email)).NotEmpty().NotNull().NotWhiteSpace();

            _context.Users.Update(new User() {UserId = Id, Email = Email});

            var isSaved = await _context.SaveChangesAsync();

            return isSaved != 0;

        }

        public async Task<bool> UpdateUserPassword(int id, string password)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();

            var hashedPassword = _authService.PasswordHasher(Password);

            _context.Users.Update(new User() { UserId = Id, Password = hashedPassword});

            var isSaved = await _context.SaveChangesAsync();

            return isSaved != 0;
        }

        public async Task<bool> CheckIfUserWithEmailExist(string email)
        {
            var Email = Guard.Argument(email, nameof(email)).NotEmpty().NotNull().NotWhiteSpace();

            var exist = await _context.Users.SingleOrDefaultAsync(x => x.Email == Email);

            return exist != null;
        }

        
    }
}
