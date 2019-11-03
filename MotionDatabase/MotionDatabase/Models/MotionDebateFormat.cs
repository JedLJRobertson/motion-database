namespace MotionDatabaseBackend.Models
{
    public class MotionDebateFormat
    {
        public int MotionId { get; set; }
        public Motion Motion { get; set; }
        public int DebateFormatId { get; set; }
        public DebateFormat DebateFormat { get; set; }
    }
}