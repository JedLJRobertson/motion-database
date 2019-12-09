using Microsoft.EntityFrameworkCore;
using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MotionParser
{
    class MotionParser
    {
        private List<Motion> motions;
        private List<MotionCategory> categories;
        private List<MotionTag> tags;

        private MotionsContext context;

        public MotionParser(MotionsContext context)
        {
            this.context = context;

            motions = context.Motions
                .Include(motion => motion.Categories)
                    .ThenInclude(categoryAssignment => categoryAssignment.Category)
                .Include(motion => motion.Tags)
                    .ThenInclude(tagAssignment => tagAssignment.MotionTag)
                .ToList();
            categories = context.MotionCategories.ToList();
            tags = context.MotionTags.ToList();
        }

        public Motion GetOrAddMotion(string motionText)
        {
            motionText = motionText.Trim();

            var match = motions.Find(motion => motion.MotionText.ToLower() == motionText.ToLower());

            if (match == null)
            {
                match = new Motion
                {
                    MotionText = motionText,
                    State = MotionState.Approved,
                };

                motions.Add(match);
                context.Motions.Add(match);
            }

            return match;
        }

        public MotionCategory GetOrAddCategory(string categoryText)
        {
            categoryText = categoryText.Trim();

            var match = categories.Find(category => category.Name.ToLower() == categoryText.ToLower());

            if (match == null)
            {
                match = new MotionCategory
                {
                    Name = categoryText,
                };

                categories.Add(match);
                context.MotionCategories.Add(match);
            }

            return match;
        }

        public MotionTag GetOrAddTag(string tagText)
        {
            tagText = tagText.Trim();

            var match = tags.Find(tag => tag.Name.ToLower() == tagText.ToLower());

            if (match == null)
            {
                match = new MotionTag
                {
                    Name = tagText,
                };

                tags.Add(match);
                context.MotionTags.Add(match);
            }

            return match;
        }

        internal void Persist()
        {
            context.SaveChanges();
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Added {motions.Count} motions, {categories.Count} categories and {tags.Count} tags.");
        }
    }
}
