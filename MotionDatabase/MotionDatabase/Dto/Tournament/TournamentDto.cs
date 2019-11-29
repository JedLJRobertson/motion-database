using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class TournamentDto
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public string Location { get; set; }
        public List<TournamentRoundDto> Rounds { get; set; }

        public TournamentDto(Tournament tournament)
        {
            Name = tournament.Name;
            Year = tournament.Year;
            Location = tournament.Location;

            Rounds = new List<TournamentRoundDto>();
            foreach (var motion in tournament.DebatedMotions)
            {
                var round = Rounds.Where(round => round.Name == motion.Round).FirstOrDefault();

                if (round == null)
                {
                    round = new TournamentRoundDto(motion.Round);
                    Rounds.Add(round);
                }

                round.Motions.Add(new MotionSearchItemDto(motion.Motion));
            }
        }
    }
}
