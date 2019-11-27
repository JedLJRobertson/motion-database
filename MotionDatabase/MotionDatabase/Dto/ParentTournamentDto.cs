using System;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class ParentTournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ParentTournamentDto(ParentTournament parentTournament)
        {
            Id = parentTournament.Id;
            Name = parentTournament.Name;
        }
    }
}
