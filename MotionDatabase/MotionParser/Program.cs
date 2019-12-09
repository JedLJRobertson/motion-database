using CsvHelper;
using Microsoft.EntityFrameworkCore;
using MotionDatabaseBackend.Models;
using MotionParser.HelloMotions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MotionParser
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var reader = new StreamReader("../../../largesample.csv"))
            using (var context = new MotionsContext(
                    new DbContextOptionsBuilder<MotionsContext>()
                    .UseMySql("server=localhost;port=3306;database=motionsdb;uid=motionsdb;password=")
                    .Options))
            using (var csv = new CsvReader(reader))
            {
                var records = csv.GetRecords<HelloMotionRow>();

                var motionParser = new MotionParser(context);

                foreach (var record in records)
                {
                    var motion = motionParser.GetOrAddMotion(record.Motion);

                    var cats = record.GetCategories();
                    foreach (var cat in cats)
                    {
                        var category = motionParser.GetOrAddCategory(cat);

                        if (!motion.Categories.Any(assignment => assignment.Category == category))
                        {
                            motion.Categories.Add(new MotionCategoryAssignment
                            {
                                Category = category,
                            });
                        }
                    }

                    if (record.Topic_Area_Specific_1 != null && record.Topic_Area_Specific_1.Length > 1)
                    {
                        var tag = motionParser.GetOrAddTag(record.Topic_Area_Specific_1);

                        if (!motion.Tags.Any(assignment => assignment.MotionTag == tag))
                        {
                            motion.Tags.Add(new MotionTagAssignment
                            {
                                MotionTag = tag,
                            });
                        }
                    }
                }

                motionParser.PrintDetails();
                motionParser.Persist();
            }
        }
    }
}
