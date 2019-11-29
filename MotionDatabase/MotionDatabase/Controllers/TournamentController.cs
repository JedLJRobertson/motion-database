using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MotionDatabaseBackend.Dto;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TournamentController : ControllerBase
    {
        private readonly MotionsContext _context;

        public TournamentController(MotionsContext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ParentTournamentDto> GetCategory(int id)
        {
            var result = _context.ParentTournaments
                .Where(pt => pt.Id == id)
                .Include(pt => pt.Tournaments)
                    .ThenInclude(t => t.DebatedMotions)
                        .ThenInclude(r => r.Motion)
                            .ThenInclude(m => m.Category)
                .Include(pt => pt.Tournaments)
                    .ThenInclude(t => t.DebatedMotions)
                        .ThenInclude(r => r.Motion)
                            .ThenInclude(m => m.Tags)
                                .ThenInclude(t => t.MotionTag)
                .Include(pt => pt.Links)
                .FirstOrDefault();

            if (result == null)
            {
                return NotFound();
            }

            return new ParentTournamentDto(result);
        }
    }
}
