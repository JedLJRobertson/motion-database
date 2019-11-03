using System.Collections.Generic;

namespace MotionDatabaseBackend.Models
{
    public enum TagStatus { Approved, WaitingApproval, Denied }
    public class MotionTag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public TagStatus Status { get; set; }
    }
}