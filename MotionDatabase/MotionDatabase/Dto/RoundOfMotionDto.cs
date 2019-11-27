using System;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class RoundOfMotionDto
    {
        public int Id { get; set; }
        public string Round { get; set; }
        public int TournamentId { get; set; }
        public string TournamentName { get; set; }
        public int TournamentYear { get; set; }
        public string TournamentLocation { get; set; }

        public RoundOfMotionDto(MotionDebateRound round)
        {
            Id = round.Id;
            Round = round.Round;
            TournamentId = round.TournamentId;
            TournamentName = round.Tournament.Name;
            TournamentYear = round.Tournament.Year;
            TournamentLocation = round.Tournament.Location;
        }
    }
}
