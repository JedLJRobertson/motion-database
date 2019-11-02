using System.Collections.Generic;

namespace MotionDatabase.Models
{
    public enum TagStatus { Approved, WaitingApproval, Denied }
    public class MotionTag
    {
        public int Id { get; set; }
        public TagStatus Status { get; set; }
    }
}