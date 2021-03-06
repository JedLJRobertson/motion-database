﻿using System.Collections.Generic;
using MotionDatabaseBackend.Models;

namespace MotionDatabaseBackend.Dto
{
    public class MotionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public IList<CategoryDto> Categories {get;set;}
        public MotionSuitability Suitability { get; set; }
        public string Language { get; set; }
        public string SourceCredit { get; set; }
        public int Difficulty { get; set; }
        public IList<TagDto> Tags { get; set; }
        public List<InfoSlideDto> InfoSlides { get; set; }
        public List<RoundOfMotionDto> Rounds { get; set; } 

        public MotionDto(Motion motion)
        {
            Id = motion.Id;
            Text = motion.MotionText;
            Suitability = motion.Suitability;
            Difficulty = (int) motion.Difficulty;
            Language = motion.LanguageCode;
            SourceCredit = motion.SourceCredit;

            Categories = new List<CategoryDto>();
            foreach (var categoryAssignment in motion.Categories)
            {
                Categories.Add(new CategoryDto(categoryAssignment.Category));
            }

            Tags = new List<TagDto>();
            foreach (var tag in motion.Tags)
            {
                Tags.Add(new TagDto(tag.MotionTag));
            }

            InfoSlides = new List<InfoSlideDto>();
            if (motion.InfoSlides != null)
            {
                foreach (var infoSlide in motion.InfoSlides)
                {
                    InfoSlides.Add(new InfoSlideDto(infoSlide));
                }
            }

            Rounds = new List<RoundOfMotionDto>();
            if (motion.DebatedRounds != null)
            {
                foreach (var round in motion.DebatedRounds)
                {
                    Rounds.Add(new RoundOfMotionDto(round));
                }
            }
        }
    }
}
