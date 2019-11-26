﻿using Microsoft.AspNetCore.Identity;
using MotionDatabaseBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabaseBackend.Helpers
{
    public class DevelopmentDatabaseSetup
    {
        public static void SetupDevelopmentDatabase(MotionsContext db)
        {
            var hasher = new PasswordHasher<User>();

            var testUser = new User
            {
                Username = "test",
                Email = "test@example.com",
                IsAdmin = true,
                IsModerator = true,
                IsConfirmed = true
            };
            testUser.PasswordHash = hasher.HashPassword(testUser, "test");
            db.Users.Add(testUser);



            var testUser2 = new User
            {
                Username = "test2",
                Email = "test2@example.com",
                IsAdmin = false,
                IsModerator = false,
                IsConfirmed = true
            };
            testUser.PasswordHash = hasher.HashPassword(testUser, "test");
            db.Users.Add(testUser);
            db.Users.Add(testUser2);

            var cat1 = new MotionCategory
            {
                Name = "International Relations",
                Description = "Motions that discuss relations between nation states."
            };
            var cat2 = new MotionCategory
            {
                Name = "Politics",
                Description = "Motions that discuss (non-IR) politics."
            };
            db.MotionCategories.Add(cat1);
            db.MotionCategories.Add(cat2);

            var tag1 = new MotionTag
            {
                Name = "Trial Tag"
            };
            db.MotionTags.Add(tag1);

            var synonymTag = new MotionTagSynonym
            {
                Name = "Synonym",
                MotionTag = tag1
            };
            db.TagSynonyms.Add(synonymTag);
            

            db.Motions.Add(new Motion
            {
                MotionText = "This house would ban religious organisations from providing rehabilitation for alcohol, drug and mental health addiction.",
                Category = cat1,
                IsExplicit = false,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.Novice
            });

            db.Motions.Add(new Motion
            {
                MotionText = "This house believes that governments should not contact ‘uncontacted’ peoples eg people living in the densely forested areas of South America, Central Africa and New Guinea.",
                Category = cat1,
                IsExplicit = false,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.Intermediate
            });

            db.Motions.Add(new Motion
            {
                MotionText = "Motion 3",
                Category = cat2,
                IsExplicit = true,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.Expert
            });

            db.Motions.Add(new Motion
            {
                MotionText = "Motion Awaiting Approval",
                Category = cat2,
                IsExplicit = false,
                State = MotionState.WaitingApproval,
                Difficulty = MotionDifficulty.Novice
            });

            var infoSlide = new MotionInfoSlide
            {
                InfoSlideText = "You're a Republican member of Congress. You incidentally come across the single copy of a unique piece of evidence, that definitively proves collusion between Donald Trump and his campaign managers with the Russian government to help Trump win the US election. If you do not disclose the evidence, it will be impossible to prove the collusion beyond doubt."
            };
            var infoSlide2 = new MotionInfoSlide
            {
                InfoSlideText = "You are Sherlock Holmes, pursuing your arch nemesis, Moriarty. Moriarty is an elusive criminal mastermind with a global network. This network is responsible for countless murders, extortions and a number of other large-scale criminal acts. You have captured Moriarty’s henchman and have enough evidence to convict him of a number of charges, including murder. Before you take him to the police, the henchman offers to give you vital information about Moriarty’s plans and whereabouts which MAY lead you to finally catch him and foil his plans for evil- in exchange for letting the henchman go free. Your deductive reasoning tells you that this is not a trap."
            };

            db.SaveChanges();
            db.Motions.Find(1).AddTag(tag1);
            db.Motions.Find(1).InfoSlides.Add(infoSlide);
            db.Motions.Find(1).InfoSlides.Add(infoSlide2);

            db.SaveChanges();
        }
    }
}
