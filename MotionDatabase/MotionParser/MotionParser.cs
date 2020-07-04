using Microsoft.EntityFrameworkCore;
using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace MotionParser
{
    class MotionParser
    {
        private List<Motion> motions;
        private List<MotionCategory> categories;
        private List<MotionTag> tags;
        private List<ParentTournament> parentTournaments;

        private MotionsContext context;

        private Regex yearGroup = new Regex(@"(\d{4})");

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

            parentTournaments = context.ParentTournaments
                .Include(pt => pt.Tournaments)
                    .ThenInclude(t => t.DebatedMotions)
                        .ThenInclude(dm => dm.Motion)
                .ToList();
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
                    SourceCredit = "HelloMotions.com",
                    Difficulty = MotionDifficulty.Uncategorised,
                    Suitability = MotionSuitability.Uncategorised,
                    LanguageCode = "en",
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

        public Tournament GetOrAddTournament(string tournamentName, string date, string location)
        {
            var parent = GetParentForTournament(tournamentName);
            var year = GetTournamentYear(tournamentName, date);

            var tournament = parent.Tournaments.FirstOrDefault(t => t.Year == year);

            if (tournament == null)
            {
                tournament = new Tournament
                {
                    ParentTournament = parent,
                    Year = year,
                    Location = location,
                    Name = tournamentName.Trim()
                };

                context.Tournaments.Add(tournament);
                parent.Tournaments.Add(tournament);
            }

            return tournament;
        }

        private ParentTournament GetParentForTournament(string tournamentName)
        {
            var parentTournamentName = Regex.Replace(tournamentName, @"\d{4}", "").Trim();

            var match = parentTournaments.FirstOrDefault(pt => pt.Name == parentTournamentName);
            if (match == null)
            {
                match = new ParentTournament
                {
                    Name = parentTournamentName,
                    Description = "",
                };

                parentTournaments.Add(match);
                context.ParentTournaments.Add(match);
            }

            return match;
        }

        private int GetTournamentYear(string tournamentName, string date)
        {
            string year;
            var match = yearGroup.Match(tournamentName);
            if (match.Success)
            {
                year = match.Groups[1].ToString();
            }
            else
            {
                year = date.Trim().Substring(6);

                if (int.Parse(year) > 60)
                {
                    year = "19" + year;
                }
                else
                {
                    year = "20" + year;
                }
            }

            return int.Parse(year);
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
