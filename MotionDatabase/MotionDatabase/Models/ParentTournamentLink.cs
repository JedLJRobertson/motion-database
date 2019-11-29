using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Models
{
    public class ParentTournamentLink
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public int ParentTournamentId { get; set; }
        public ParentTournament ParentTournament { get; set; }
    }
}
