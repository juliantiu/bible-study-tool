/*
 * dotnet ef migrations add MyFirstMigration2 --project BibleStudyTool.Infrastructure --startup-project BibleStudyTool.Public --context BibleReadingDbContext --output-dir ./Data/Migrations
 * dotnet ef database update --project BibleStudyTool.Infrastructure --startup-project BibleStudyTool.Public
 * */

using System;
using System.IO;
using System.Reflection;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.BibleAggregate;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;

namespace BibleStudyTool.Infrastructure.Data
{
    public class BibleReadingDbContext: DbContext
    {
        public BibleReadingDbContext(DbContextOptions<BibleReadingDbContext> options) : base(options)
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
        public DbSet<BibleVerseNote> BibleVerseNotes { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
