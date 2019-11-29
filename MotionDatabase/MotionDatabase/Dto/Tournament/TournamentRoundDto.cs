using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class TournamentRoundDto
    {
        public int Id { get; set; }
        public List<MotionSearchItemDto> Motions { get; set; }
        public string Name { get; set; }

        public TournamentRoundDto(string roundName, int id)
        {
            Id = id;
            Name = roundName;
            Motions = new List<MotionSearchItemDto>();
        }
    }
}
