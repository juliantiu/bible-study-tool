using System;
using System.Reflection;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.BibleAggregate;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;

namespace BibleStudyTool.Infrastructure.Data
{
    public class BibleReadingContext: DbContext
    {
        public BibleReadingContext(DbContextOptions<BibleReadingContext> options) : base(options)
        {
        }

        // BibleAggregate
        public DbSet<Language> Languages { get; set; }
        public DbSet<BibleVersion> BibleVersions { get; set; }
        public DbSet<BibleBook> BibleBooks { get; set; }
        public DbSet<BibleBookAbbreviation> BibleBookAbbreviations { get; set; }
        public DbSet<BibleVerse> BibleVerses { get; set; }

        // BibleReaderAggregate
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagGroup> TagGroups { get; set; }

        // Join Entities
        public DbSet<TagGroupNote> TagGroupNotes { get; set; }
        public DbSet<TagGroupTag> TagGroupTags { get; set; }
        public DbSet<TagNote> TagNotes { get; set; }
        public DbSet<BibleVerseNote> VerseNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
