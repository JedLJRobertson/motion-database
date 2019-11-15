
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MotionDatabaseBackend.Dto;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController
    {
        private readonly MotionsContext _context;

        public TagController(MotionsContext context)
        {
            _context = context;
        }

        [HttpGet("{query}")]
        public ActionResult<List<TagDto>> GetMatchingTag(string query)
        {
            var response = new List<TagDto>();
            _context.MotionTags.Where(tag => tag.Name.StartsWith(query)).ToList().ForEach(tag => response.Add(new TagDto(tag)));
            return response;
        }
    }
}
