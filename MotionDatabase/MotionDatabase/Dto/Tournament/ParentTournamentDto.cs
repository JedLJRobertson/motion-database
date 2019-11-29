using System;
using System.Collections.Generic;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class ParentTournamentDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Links { get; set; }
        public List<string> LinkDescriptions { get; set; }
        public List<TournamentDto> Tournaments { get; set; }

        public ParentTournamentDto(ParentTournament parentTournament)
        {
            Id = parentTournament.Id;
            Name = parentTournament.Name;
            Description = parentTournament.Description;

            Links = new List<string>();
            LinkDescriptions = new List<string>();
            foreach (var link in parentTournament.Links)
            {
                Links.Add(link.Url);
                LinkDescriptions.Add(link.Description);
            }

            Tournaments = new List<TournamentDto>();
            foreach (var tournament in parentTournament.Tournaments)
            {
                Tournaments.Add(new TournamentDto(tournament));
            }
        }
    }
}
