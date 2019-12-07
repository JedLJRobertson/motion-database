using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Dto
{
    public class MotionSearchItemDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool IsExplicit { get; set; }
        public int Difficulty { get; set; }
        public IList<CategoryDto> Categories { get; set; }
        public IList<TagDto> Tags { get; set; }

        public MotionSearchItemDto(Motion motion)
        {
            Id = motion.Id;
            Text = motion.MotionText;
            IsExplicit = motion.IsExplicit;
            Difficulty = (int)motion.Difficulty;

            Tags = new List<TagDto>();
            foreach (var tag in motion.Tags)
            {
                Tags.Add(new TagDto(tag.MotionTag));
            }

            Categories = new List<CategoryDto>();
            foreach (var categoryAssignment in motion.Categories)
            {
                Categories.Add(new CategoryDto(categoryAssignment.Category));
            }
        }
    }
}
