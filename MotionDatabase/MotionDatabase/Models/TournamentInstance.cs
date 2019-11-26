using System.Collections.Generic;

namespace MotionDatabaseBackend.Models
{
    public class TournamentInstance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }
        public string Location { get; set; }

        public int OrganisationId;
        public DebatingOrganisation Organisation { get; set; }

        public int ParentTournamentId;
        public Tournament ParentTournament { get; set; }
        public int FormatId { get; set; }
        public DebateFormat Format { get; set; }

        public IList<MotionDebateRound> DebatedMotions { get; } = new List<MotionDebateRound>();
    }
}