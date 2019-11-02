namespace MotionDatabase.Models
{
    public enum TagStatus { Approved, WaitingApproval, Denied }
    public class MotionTag
    {
        public int TagId { get; set; }
        public TagStatus Status { get; set; }
        public IList<MotionTagRelated> RelatedTags { get; set; }
    }
}