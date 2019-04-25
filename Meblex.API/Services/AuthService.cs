using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Dawn;
using Meblex.API.Context;
using Meblex.API.DTO;
using Meblex.API.Helper;
using Meblex.API.Interfaces;
using Meblex.API.Models;
using Microsoft.EntityFrameworkCore;
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
            var Login = Guard.Argument(login, nameof(login)).NotEmpty().NotNull().NotWhiteSpace();
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();
            var hashedPassword = PasswordHasher(Password);
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Login && x.Password == hashedPassword);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.AccessTokenSecret, _jwtSettings.AccessTokenExpiredHours);
        }

        public async Task<string> GetAccessToken(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();

            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == Id);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.AccessTokenSecret, _jwtSettings.AccessTokenExpiredHours);
        }

        public async Task<string> GetRefreshToken(string login, string password)
        {
            var Login = Guard.Argument(login, nameof(login)).NotEmpty().NotNull().NotWhiteSpace();
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();

            var hashedPassword = PasswordHasher(Password);
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Login && x.Password == hashedPassword);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.RefreshTokenSecret, _jwtSettings.RefreshTokenExpiredHours);

        }

        public async Task<string> GetRefreshToken(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == Id);
            if (dbUser == null) return null;

            return GenerateToken(dbUser, _jwtSettings.RefreshTokenSecret, _jwtSettings.RefreshTokenExpiredHours);

        }

        private string GenerateToken(User dbUser, string secret, int expiredHours)
        {
            var Secret = Guard.Argument(secret, nameof(secret)).NotEmpty().NotNull().NotWhiteSpace();
            var ExpiredHours = Guard.Argument(expiredHours, nameof(expiredHours)).NotNegative();
            var DbUser = Guard.Argument(dbUser, nameof(dbUser)).NotNull().Value;
            
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, DbUser.UserId.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(ExpiredHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var response = tokenHandler.WriteToken(token);

            return response;
        }



        public async Task<UserConfirmedRegistation> RegisterNewUser(UserRegisterForm userRegisterForm)
        {
            var UserRegisterForm = Guard.Argument(userRegisterForm, nameof(userRegisterForm)).NotNull().Value;
            var user = new User()
            {
                Email = UserRegisterForm.Email,
                Password = PasswordHasher(UserRegisterForm.Password),
                Role = "Client"
            };

            _context.Users.Add(user);

            var client = new Client()
            {
                UserId = user.UserId,
                Address = UserRegisterForm.Address,
                City = UserRegisterForm.City,
                Name = UserRegisterForm.Name,
                PostCode = int.Parse(UserRegisterForm.PostCode),
                State = UserRegisterForm.State,
                NIP = int.Parse(UserRegisterForm.NIP)
            };

            _context.Clients.Add(client);


            if (await _context.SaveChangesAsync() != 0)
            {
                return new UserConfirmedRegistation() { Login = user.Email };
            }

            return null;

        }

        private string PasswordHasher(string password)
        {
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();
            var md5Hasher = MD5.Create();
            var passwordInBytes = Encoding.ASCII.GetBytes(Password);
            var hashed = md5Hasher.ComputeHash(passwordInBytes);

            return Encoding.ASCII.GetString(hashed);
        }
    }
}