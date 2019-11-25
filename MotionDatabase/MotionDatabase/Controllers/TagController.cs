
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotionDatabaseBackend.Dto;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagController : ControllerBase
    {
        private readonly MotionsContext _context;

        public TagController(MotionsContext context)
        {
            _context = context;
        }

        [HttpGet("query/{query}")]
        public ActionResult<List<TagDto>> GetMatchingTag(string query)
        {
            var response = new List<TagDto>();

            _context.MotionTags.Where(tag => tag.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase)).ToList().ForEach(tag => response.Add(new TagDto(tag)));

            _context.TagSynonyms
                .Where(synTag => synTag.Name.Contains(query, StringComparison.InvariantCultureIgnoreCase))
                .Include(synTag => synTag.MotionTag)
                .ToList()
                .ForEach(synTag =>
                {
                    if (!response.Any(tag => tag.Id == synTag.MotionTag.Id))
                    {
                        response.Add(new TagDto(synTag.MotionTag));
                    }
                });

            return response;
        }

        [HttpGet("{id}")]
        public ActionResult<TagDto> GetTag(int id)
        {
            var result = _context.MotionTags
                .Include(tag => tag.MotionTagSynonyms)
                .Where(tag => tag.Id == id)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            return new TagDto(result);
        }
    }
}
