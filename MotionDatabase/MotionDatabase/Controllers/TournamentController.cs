using System;
using System.Collections.Generic;
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
        public ActionResult<ParentTournamentDto> GetTournament(int id)
        {
            var result = _context.ParentTournaments
                .Where(pt => pt.Id == id)
                .Include(pt => pt.Tournaments)
                    .ThenInclude(t => t.DebatedMotions)
                        .ThenInclude(r => r.Motion)
                            .ThenInclude(m => m.Categories)
                                .ThenInclude(mca => mca.Category)
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

        [HttpGet]
        public ActionResult<List<ParentTournamentDto>> GetTournaments()
        {
            var result = new List<ParentTournamentDto>();

            _context.ParentTournaments.ToList().ForEach(pt => result.Add(new ParentTournamentDto(pt)));

            return result;
        }
    }
}
