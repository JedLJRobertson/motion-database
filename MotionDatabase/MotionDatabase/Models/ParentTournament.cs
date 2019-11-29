using System.Collections.Generic;

namespace MotionDatabaseBackend.Models
{
    public class ParentTournament
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int? OrganisationId { get; set; }
        public DebatingOrganisation Organisation { get; set; }

        public IList<Tournament> Tournaments { get; } = new List<Tournament>();
        public IList<ParentTournamentLink> Links { get; } = new List<ParentTournamentLink>();
    }
}