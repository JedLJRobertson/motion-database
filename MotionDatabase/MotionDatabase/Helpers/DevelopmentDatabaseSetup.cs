using Microsoft.AspNetCore.Identity;
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

            db.SaveChanges();
            db.Motions.Find(1).AddTag(tag1);

            db.SaveChanges();
        }
    }
}
