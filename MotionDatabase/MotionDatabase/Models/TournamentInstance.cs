namespace MotionDatabaseBackend.Models
{
    public class TournamentInstance
    {
        public int Id { get; set; }
        public int Name { get; set; }
        public int Year { get; set; }
        public string Location { get; set; }

        public int OrganisationId;
        public DebatingOrganisation Organisation { get; set; }

        public int ParentTournamentId;
        public TournamentInstance ParentTournament { get; set; }
        public int FormatId { get; set; }
        public DebateFormat Format { get; set; }
    }
}