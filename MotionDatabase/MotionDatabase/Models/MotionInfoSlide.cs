using System;
namespace MotionDatabaseBackend.Models
{
    public class MotionInfoSlide
    {
        public int Id { get; set; }
        public int MotionId { get; set; }
        public Motion Motion { get; set; }
        public string InfoSlideText { get; set; }
    }
}
