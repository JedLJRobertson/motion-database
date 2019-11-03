using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotionDatabaseBackend.Dto;
using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MotionController
    {
        private readonly MotionsContext _context;

        public MotionController(MotionsContext context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("search")]
        public ActionResult<MotionSearchResultDto> Search(MotionSearchDto request)
        {
            var query = _context.Motions.AsQueryable();

            if (request.CategoryId != null)
            {
                query = query.Where(m => m.CategoryId == request.CategoryId);
            }

            if (request.Tags != null)
            {
                if (request.AllTags)
                {
                    query = query.Where(m => request.Tags.All(t => m.Tags.Any(mt => mt.MotionTagId == t)));
                } 
                else
                {
                    query = query.Where(m => request.Tags.Any(t => m.Tags.Any(mt => mt.MotionTagId == t)));
                }
            }

            query = query
                .Where(m => m.State == MotionState.Approved)
                .Include(m => m.Category)
                .Include(m => m.Tags)
                    .ThenInclude(t => t.MotionTag);


            var result = new MotionSearchResultDto(query.ToList())
            {
                Query = request
            };
            return result;
        }
    }
}
