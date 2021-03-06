﻿using System;
using System.Collections.Generic;

namespace MotionDatabaseBackend.Models
{
    public enum MotionState { Approved, WaitingApproval, Denied }
    public enum MotionDifficulty { Uncategorised, NoviceSchools, NoviceUni, Intermediate, Advanced }
    public enum MotionSuitability { AllAges, Explicit, Uncategorised }
    public class Motion
    {
        public int Id { get; set; }
        public string MotionText { get; set; }
        public MotionSuitability Suitability { get; set; }
        public MotionState State { get; set; }
        public MotionDifficulty Difficulty { get; set; }
        public string SourceCredit { get; set; }
        public string LanguageCode { get; set; }

        public IList<MotionCategoryAssignment> Categories { get; set; } = new List<MotionCategoryAssignment>(); 

        public IList<MotionInfoSlide> InfoSlides { get; set; } = new List<MotionInfoSlide>();

        public IList<MotionDebateFormat> DebateFormats { get; } = new List<MotionDebateFormat>();

        public IList<MotionTagAssignment> Tags { get; } = new List<MotionTagAssignment>();
        public IList<MotionDebateRound> DebatedRounds { get; } = new List<MotionDebateRound>();

        internal void AddTag(MotionTag tag)
        {
            this.Tags.Add(new MotionTagAssignment
            {
                Motion = this,
                MotionTag = tag
            });
        }
    }
}