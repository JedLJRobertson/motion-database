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
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public bool IsExplicit { get; set; }
        public int Difficulty { get; set; }
        public IList<TagDto> Tags { get; set; }

        public MotionSearchItemDto(Motion motion)
        {
            Id = motion.Id;
            Text = motion.MotionText;
            Category = motion.Category.Name;
            CategoryId = motion.CategoryId;
            IsExplicit = motion.IsExplicit;
            Difficulty = (int)motion.Difficulty;
            Tags = new List<TagDto>();

            foreach (var tag in motion.Tags)
            {
                Tags.Add(new TagDto(tag.MotionTag));
            }
        }
    }
}
