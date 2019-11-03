using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Models
{
    public class MotionDebateRound
    {
        public int Id { get; set; }
        public string Round { get; set; }
        public int TournamentId { get; set; }
        public TournamentInstance Tournament { get; set; }
        public int MotionId { get; set; }
        public Motion Motion { get; set; }
    }
}
