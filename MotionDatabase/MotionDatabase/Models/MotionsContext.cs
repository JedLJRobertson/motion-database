using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotionDatabase.Models
{
    public class MotionsContext : DbContext 
    {
        public DbSet<Motion> Motions { get; set; }
        public DbSet<DebateFormat> DebateFormats { get; set; }
        public DbSet<MotionTag> MotionTags { get; set; }
        public DbSet<MotionCategory> MotionCategories { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }
        public DbSet<TournamentInstance> TournamentInstances { get; set; }
        public DbSet<DebatingOrganisation> Organisations { get; set; }

        public MotionsContext(DbContextOptions<MotionsContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<MotionTagAssignment>().HasKey(mta => new { mta.MotionTagId, mta.MotionId });
            modelBuilder.Entity<MotionDebateFormat>().HasKey(mdf => new { mdf.DebateFormatId, mdf.MotionId });
            modelBuilder.Entity<MotionTagRelated>().HasKey(mtr => new { mtr.TagAId, mtr.TagBId });
        }
    }
}
