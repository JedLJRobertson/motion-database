using System.Collections.Generic;

namespace MotionDatabaseBackend.Models
{
    public class Tournament
    {
        public int Id { get; set; }
        public int Name { get; set; }

        public int OrganisationId { get; set; }
        public DebatingOrganisation Organisation { get; set; }

        public IList<TournamentInstance> Instances { get; } = new List<TournamentInstance>();
    }
}