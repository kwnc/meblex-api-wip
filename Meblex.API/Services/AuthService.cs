using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Meblex.API.Context;
using Meblex.API.DTO;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Meblex.API.Services
{
    public class AuthService:IAuthService
    {
        private readonly MeblexDbContext _context;
        private readonly JWTSettings _jwtSettings;

        public AuthService(MeblexDbContext context, JWTSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        public async Task<string> GetAccessToken(string login, string password)
        {
            var hashedPassword = PasswordHasher(password);
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == login && x.Password == hashedPassword);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.AccessTokenSecret, _jwtSettings.AccessTokenExpiredHours);
        }

        public async Task<string> GetAccessToken(int id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.AccessTokenSecret, _jwtSettings.AccessTokenExpiredHours);
        }

        public async Task<string> GetRefreshToken(string login, string password)
        {
            var hashedPassword = PasswordHasher(password);
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == login && x.Password == hashedPassword);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.RefreshTokenSecret, _jwtSettings.RefreshTokenExpiredHours);

        }

        public async Task<string> GetRefreshToken(int id)
        {
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.RefreshTokenSecret, _jwtSettings.RefreshTokenExpiredHours);

        }

        private string GenerateToken(User dbUser, string secret, int expiredHours)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, dbUser.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(expiredHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var response = tokenHandler.WriteToken(token);

            return response;
        }



        public async Task<UserConfirmedRegistation> RegisterNewUser(UserRegisterForm userRegisterForm)
        {
            var user = new User(){Email = userRegisterForm.Email, Password = PasswordHasher(userRegisterForm.Password), Role = "Client"};

            _context.Users.Add(user);

            var client = new Client(){ UserId = user.UserId, Address = userRegisterForm.Address, City = userRegisterForm.City, Name = userRegisterForm.Name, PostCode = userRegisterForm.PostCode, State = userRegisterForm.State};

            _context.Clients.Add(client);


            if (await _context.SaveChangesAsync() != 0)
            {
                return new UserConfirmedRegistation() { Login = user.Email };
            }

            return null;

        }

        private string PasswordHasher(string password)
        {
            var md5Hasher = MD5.Create();
            var passwordInBytes = Encoding.ASCII.GetBytes(password);
            var hashed = md5Hasher.ComputeHash(passwordInBytes);

            return Encoding.ASCII.GetString(hashed);
        }
    }
}