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
    public class MotionController : ControllerBase
    {
        private readonly MotionsContext _context;

        public MotionController(MotionsContext context)
        {
            _context = context;
        }

        private IEnumerable<Motion> QueryByAgeSuitability(IEnumerable<Motion> query, int suitabilityMode, bool includeUncategorised)
        {
            if (suitabilityMode == 1)
            {
                query = query.Where(m => m.Suitability == MotionSuitability.AllAges ||
                    (includeUncategorised && m.Suitability == MotionSuitability.Uncategorised));
            }
            else if (suitabilityMode == 2)
            {
                query = query.Where(m => m.Suitability == MotionSuitability.Explicit ||
                    (includeUncategorised && m.Suitability == MotionSuitability.Uncategorised));
            }
            return query;
        }

        private IEnumerable<Motion> QueryByDifficulty(IEnumerable<Motion> query, List<MotionDifficulty> difficulties)
        {
            if (difficulties != null && difficulties.Count > 0)
            {
                query = query.Where(m => difficulties.Contains(m.Difficulty));
            }

            return query;
        }
            
        [HttpPost]
        [Route("search")]
        public ActionResult<MotionSearchResultDto> Search(MotionSearchDto request)
        {
            var query = _context.Motions
                .Where(m => m.State == MotionState.Approved)
                .Include(m => m.Categories)
                    .ThenInclude(mca => mca.Category)
                .Include(m => m.Tags)
                    .ThenInclude(t => t.MotionTag)
                .AsEnumerable();

            if (request.Categories != null && request.Categories.Count > 0)
            {
                query = query.Where(m => request.Categories.Any(cat => m.Categories.Any(mca => mca.CategoryId == cat)));
            }

            query = QueryByAgeSuitability(query, request.SuitabilityMode, request.SuitabilityIncludeUncategorised);

            query = QueryByDifficulty(query, request.Difficulties);

            if (request.Tags != null && request.Tags.Count > 0)
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

            var totalCount = query.Count();

            query = query
                .Skip(request.StartFrom != null ? (int)request.StartFrom : 0)
                .Take(request.NumberResults != null ? (int) request.NumberResults : 20);

            var result = new MotionSearchResultDto(query.ToList())
            {
                Query = request,
                CountResults = totalCount,
            };
            return result;

        }

        [HttpGet("{id}")]
        public ActionResult<MotionDto> GetMotion(int id)
        {
            var result = _context.Motions
                .Where(m => m.Id == id)
                .Include(m => m.Categories)
                    .ThenInclude(mca => mca.Category)
                .Include(m => m.Tags)
                    .ThenInclude(mt => mt.MotionTag)
                .Include(m => m.InfoSlides)
                .Include(m => m.DebatedRounds)
                    .ThenInclude(r => r.Tournament)
                        .ThenInclude(t => t.ParentTournament)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            return new MotionDto(result);
        }
    }
}
