using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class MotionSearchResultDto
    {
        public MotionSearchDto Query { get; set; }
        public List<MotionSearchItemDto> Results { get; set; }

        public MotionSearchResultDto(List<Motion> motions)
        {
            Results = new List<MotionSearchItemDto>();

            foreach (var motion in motions)
            {
                var motionDto = new MotionSearchItemDto
                {
                    Id = motion.Id,
                    Text = motion.MotionText,
                    Category = motion.Category.Name,
                    CategoryId = motion.CategoryId,
                    Tags = new List<TagDto>()
                };

                foreach (var tag in motion.Tags)
                {
                    motionDto.Tags.Add(new TagDto(tag.MotionTag));
                }

                Results.Add(motionDto);
            }
        }
    }
}
