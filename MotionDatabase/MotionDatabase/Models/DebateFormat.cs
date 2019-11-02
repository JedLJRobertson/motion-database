using System.Collections.Generic;

namespace MotionDatabase.Models
{
    public class DebateFormat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<MotionDebateFormat> Motions { get; set; }
    }
}