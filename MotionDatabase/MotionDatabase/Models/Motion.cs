using System.Collections.Generic;

namespace MotionDatabase.Models
{
    public enum MotionState { Approved, WaitingApproval, Denied }
    public enum MotionDifficulty { Novice, Intermediate, Expert }
    public class Motion
    {
        public int Id { get; set; }
        public string MotionText { get; set; }
        public bool IsExplicit { get; set; }
        public MotionState State { get; set; }
        public MotionDifficulty Difficulty { get; set; }

        public  int CategoryId { get; set; }
        public MotionCategory Category { get; set; }

        public IList<MotionDebateFormat> DebateFormats { get; set; }

        public IList<MotionTagAssignment> Tags { get; set; }
    }
}