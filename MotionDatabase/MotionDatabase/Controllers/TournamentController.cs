using System;
using Microsoft.AspNetCore.Mvc;
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
            var result = _context.ParentTournaments.Find(id);

            if (result == null)
            {
                return NotFound();
            }

            return new ParentTournamentDto(result);
        }
    }
}
