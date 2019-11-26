using System;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class InfoSlideDto
    {
        public string Text { get; set; }

        public InfoSlideDto(MotionInfoSlide infoSlide)
        {
            Text = infoSlide.InfoSlideText;
        }
    }
}
