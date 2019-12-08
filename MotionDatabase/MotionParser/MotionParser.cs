using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MotionParser
{
    class MotionParser
    {
        private List<Motion> motions;
        private List<MotionCategory> categories;
        private List<MotionTag> tags;

        public MotionParser()
        {
            motions = new List<Motion>();
            categories = new List<MotionCategory>();
            tags = new List<MotionTag>();
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
            }

            return match;
        }

        internal void Persist(MotionsContext db)
        {
            db.MotionCategories.AddRange(categories);
            db.MotionTags.AddRange(tags);
            db.Motions.AddRange(motions);

            db.SaveChanges();
        }

        public void PrintDetails()
        {
            Console.WriteLine($"Added {motions.Count} motions, {categories.Count} categories and {tags.Count} tags.");
        }
    }
}
