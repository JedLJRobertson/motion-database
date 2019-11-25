using System.Collections.Generic;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class MotionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public string CategoryDescription { get; set; }
        public bool IsExplicit { get; set; }
        public int Difficulty { get; set; }
        public IList<TagDto> Tags { get; set; }

        public MotionDto(Motion motion)
        {
            Id = motion.Id;
            Text = motion.MotionText;
            CategoryId = motion.CategoryId;
            Category = motion.Category.Name;
            CategoryDescription = motion.Category.Description;
            IsExplicit = motion.IsExplicit;
            Difficulty = (int) motion.Difficulty;

            Tags = new List<TagDto>();
            foreach (var tag in motion.Tags)
            {
                Tags.Add(new TagDto(tag.MotionTag));
            }
        }
    }
}
