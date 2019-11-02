using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MotionDatabase.Dto;
using MotionDatabase.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabase.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController
    {
        private readonly MotionsContext _context;
        private readonly PasswordHasher<User> _hasher;

        public UserController(MotionsContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult<AuthTokenDto> Login(LoginDto login)
        {
            var user = _context.Users.First(u => u.Username.ToLower() == login.Username.ToLower());

            if (user == null)
            {
                return new NotFoundResult();
            }

            if (_hasher.VerifyHashedPassword(user, user.PasswordHash, login.Password) == PasswordVerificationResult.Success)
            {
                return new NotFoundResult();
            }

            // authentication successful so generate jwt token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
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
            user.Token = tokenHandler.WriteToken(token);

            return new AuthTokenDto(token);
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
