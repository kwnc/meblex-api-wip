using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
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
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.IdentityModel.Tokens;

namespace Meblex.API.Services
{
    public class AuthService:IAuthService
    {
        private readonly MeblexDbContext _context;
        private readonly JWTSettings _jwtSettings;
        private readonly IStringLocalizer<AuthService> _localizer;


        public AuthService(MeblexDbContext context, JWTSettings jwtSettings, IStringLocalizer<AuthService> localizer)
        {
            _context = context;
            _jwtSettings = jwtSettings;
            _localizer = localizer;
        }

        public async Task<string> GetAccessToken(string login, string password)
        {
            var Login = Guard.Argument(login, nameof(login)).NotEmpty().NotNull().NotWhiteSpace();
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();
            var hashedPassword = PasswordHasher(Password);
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Login && x.Password == hashedPassword);
            if (dbUser == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["User not found"]);

            return GenerateToken(dbUser, _jwtSettings.AccessTokenSecret, _jwtSettings.AccessTokenExpiredHours);
        }

        public async Task<string> GetAccessToken(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();

            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == Id);
            if (dbUser == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["User not found"]);

            return GenerateToken(dbUser, _jwtSettings.AccessTokenSecret, _jwtSettings.AccessTokenExpiredHours);
        }

        public async Task<string> GetRefreshToken(string login, string password)
        {
            var Login = Guard.Argument(login, nameof(login)).NotEmpty().NotNull().NotWhiteSpace();
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();

            var hashedPassword = PasswordHasher(Password);
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == Login && x.Password == hashedPassword);
            if (dbUser == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["User not found"]);

            return GenerateToken(dbUser, _jwtSettings.RefreshTokenSecret, _jwtSettings.RefreshTokenExpiredHours);

        }

        public async Task<string> GetRefreshToken(int id)
        {
            var Id = Guard.Argument(id, nameof(id)).NotNegative();
            var dbUser = await _context.Users.FirstOrDefaultAsync(x => x.UserId == Id);
            if (dbUser == null) throw new HttpStatusCodeException(HttpStatusCode.NotFound, _localizer["User not found"]);

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
                    new Claim(ClaimTypes.Name, DbUser.UserId.ToString()),
                    new Claim(ClaimTypes.Role, DbUser.Role)
                }),
                Expires = DateTime.UtcNow.AddHours(ExpiredHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var response = tokenHandler.WriteToken(token);

            return response;
        }



        public async Task<UserConfirmedRegistation> RegisterNewUser(AuthRegisterForm authRegisterForm)
        {
            var UserRegisterForm = Guard.Argument(authRegisterForm, nameof(authRegisterForm)).NotNull().Value;
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
                NIP = UserRegisterForm.NIP
            };

            _context.Clients.Add(client);


            if (await _context.SaveChangesAsync() != 0)
            {
                return new UserConfirmedRegistation() { Login = user.Email };
            }

            throw new HttpStatusCodeException(HttpStatusCode.InternalServerError, _localizer["User couldn't be registered"]);

        }

        public async Task<bool> CheckUser(string email, string password)
        {
            var Email = Guard.Argument(email, nameof(email)).NotEmpty().NotWhiteSpace().NotNull();
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();


            var user = await _context.Users.AnyAsync(x =>
                x.Email == Email && x.Password == PasswordHasher(Password));

            return user;

        }

        public string PasswordHasher(string password)
        {
            var Password = Guard.Argument(password, nameof(password)).NotEmpty().NotNull().NotWhiteSpace();
            var md5Hasher = MD5.Create();
            var passwordInBytes = Encoding.ASCII.GetBytes(Password);
            var hashed = md5Hasher.ComputeHash(passwordInBytes);

            return Encoding.ASCII.GetString(hashed);
        }
    }
}