using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class TournamentRoundDto
    {
        public List<MotionSearchItemDto> Motions { get; set; }
        public string Name { get; set; }

        public TournamentRoundDto(string roundName)
        {
            Name = roundName;
            Motions = new List<MotionSearchItemDto>();
        }
    }
}
