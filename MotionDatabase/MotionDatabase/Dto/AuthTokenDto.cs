using MotionDatabase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabase.Dto
{
    public class AuthTokenDto
    {
        public string Id { get; set; }
        public long Expires { get; set; }

        public AuthTokenDto()
        {

        }

        public AuthTokenDto(AuthToken token)
        {
            Id = token.Id;
            Expires = token.Expires;
        }
    }
}
