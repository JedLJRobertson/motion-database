using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MotionDatabaseBackend.Dto;
using MotionDatabaseBackend.Helpers;
using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly MotionsContext _context;
        private readonly PasswordHasher<User> _hasher;
        private readonly AppSettings _appSettings;

        public UserController(MotionsContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
            _hasher = new PasswordHasher<User>();
        }

        [HttpPost]
        public ActionResult<LoggedInDto> Login(LoginDto login)
        {
            var user = _context.Users.First(u => u.Username.ToLower() == login.Username.ToLower());

            if (user == null)
            {
                return new NotFoundResult();
            }

            if (_hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) != PasswordVerificationResult.Success)
            {
                return new ForbidResult();
            }

            // Issue JWT
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.JWTSecret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new LoggedInDto(user, tokenHandler.WriteToken(token));
        }

        protected bool AddUser(string username, string email, string password)
        {
            if (_context.Users.First(u => u.Username.ToLower() == username.ToLower()) != null)
            {
                return false;
            }

            if (_context.Users.First(u => u.Email.ToLower() == email.ToLower()) != null)
            {
                return false;
            }

            var user = new User
            {
                Username = username,
                Email = email
            };

            var hashed = _hasher.HashPassword(user, password);

            user.PasswordHash = hashed;

            _context.Users.Add(user);
            _context.SaveChanges();

            return true;
        }
    }
}
