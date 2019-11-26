using System;
namespace MotionDatabaseBackend.Models
{
    public class MotionTagSynonym
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MotionTagId { get; set; }
        public MotionTag MotionTag { get; set; }
    }
}
