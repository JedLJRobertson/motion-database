using MotionDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class LoggedInDto
    {
        public string Username { get; set; }
        public string Token { get; set; }

        public LoggedInDto(User user, string token) 
        {
            Username = user.Username;
            Token = token;
        }
    }
}
