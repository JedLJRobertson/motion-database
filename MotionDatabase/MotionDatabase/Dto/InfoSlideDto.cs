using System;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class InfoSlideDto
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public InfoSlideDto(MotionInfoSlide infoSlide)
        {
            Id = infoSlide.Id;
            Text = infoSlide.InfoSlideText;
        }
    }
}
