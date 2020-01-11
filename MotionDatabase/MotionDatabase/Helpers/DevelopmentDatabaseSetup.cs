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

            var synonymTag = new MotionTagSynonym
            {
                Name = "Synonym",
                MotionTag = tag1
            };
            db.TagSynonyms.Add(synonymTag);
            
            db.Motions.Add(new Motion
            {
                MotionText = "This house would ban religious organisations from providing rehabilitation for alcohol, drug and mental health addiction.",
                Suitability = MotionSuitability.AllAges,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.NoviceSchools
            });

            var motion2 = new Motion
            {
                MotionText = "This house believes that governments should not contact ‘uncontacted’ peoples eg people living in the densely forested areas of South America, Central Africa and New Guinea.",
                Suitability = MotionSuitability.AllAges,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.Intermediate
            };
            motion2.Categories.Add(new MotionCategoryAssignment
            {
                Category = cat1
            });
            db.Motions.Add(motion2);

            var motion3 = new Motion
            {
                MotionText = "Motion 3",
                Suitability = MotionSuitability.Explicit,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.Advanced
            };
            motion3.Categories.Add(new MotionCategoryAssignment
            {
                Category = cat1
            });
            motion3.Categories.Add(new MotionCategoryAssignment
            {
                Category = cat2
            });
            db.Motions.Add(motion3);

            var motion4 = new Motion
            {
                MotionText = "Motion Awaiting Approval",
                Suitability = MotionSuitability.Uncategorised,
                State = MotionState.Approved,
                Difficulty = MotionDifficulty.NoviceUni
            };
            motion4.Categories.Add(new MotionCategoryAssignment
            {
                Category = cat2
            });
            db.Motions.Add(motion4);

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

            var format = new DebateFormat
            {
                Name = "British Parliamentary"
            };
            var parentTournament = new ParentTournament
            {
                Name = "Australs",
                Description = "The Australasian Intervarsity Debating Championships (known colloquially as \"Australs\") is an annual debating tournament for teams from universities in the Australasian region. It is one of the world's largest debating tournaments, second only in size to the World Universities Debating Championship (WUDC), the European Universities Debating Championships (EUDC) and one of the largest annual student events in the world. Australs follows the Australia-Asian Debating format (three speakers plus replies), rather than the British Parliamentary Style used at WUDC. It is held every year in early-July under the auspices of the Australasian Intervarsity Debating Association (AIDA). The host university is selected a year before at a meeting of the Council of the Australasian Intervarsity Debating Association."
            };
            var link = new ParentTournamentLink
            {
                Url = "google.com",
                Description = "Test Link",
            };
            parentTournament.Links.Add(link);
            db.ParentTournaments.Add(parentTournament);
            var tournamentInst = new Tournament
            {
                Name = "Australs 2020",
                Year = 2020,
                Location = "Auckland",
                ParentTournament = parentTournament,
                Format = format,
            };
            db.SaveChanges();
            db.ParentTournaments.Find(1).Tournaments.Add(tournamentInst);
            var round = new MotionDebateRound
            {
                Round = "Semi Final",
                Tournament = tournamentInst,
            };
            var round2 = new MotionDebateRound
            {
                Round = "1st Round (Open)",
                Tournament = tournamentInst,
            };
            var round3 = new MotionDebateRound
            {
                Round = "1st Round (Open)",
                Tournament = tournamentInst,
            };

            db.Motions.Find(1).DebatedRounds.Add(round);
            db.Motions.Find(1).DebatedRounds.Add(round2);
            db.Motions.Find(2).DebatedRounds.Add(round3);

            db.SaveChanges();
        }
    }
}
