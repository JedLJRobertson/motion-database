using System.Collections.Generic;

namespace MotionDatabaseBackend.Models
{
    public class DebateFormat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<MotionDebateFormat> Motions { get; } = new List<MotionDebateFormat>();
    }
}