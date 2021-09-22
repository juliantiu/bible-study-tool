/*
 * * EF core migration and update commands from cli
 * ** dotnet ef migrations add MyFirstMigration --project BibleStudyTool.Infrastructure --startup-project BibleStudyTool.Public --context BibleReadingDbContext --output-dir ./Data/Migrations
 * ** dotnet ef database update --project BibleStudyTool.Infrastructure --startup-project BibleStudyTool.Public
 * * Clearing All DB tables
 * 
 *  DROP SCHEMA public CASCADE;
 *  CREATE SCHEMA public;
 *  
 * Microsoft Identity with EF core
 *   - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-5.0&tabs=visual-studio
 * */

using System;
using System.Reflection;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BibleStudyTool.Infrastructure.Data
{
    /*
     * Uses the default IdentiyDbContext.
     * See for customizations (of user primary key, indentity role type, etc.):
     *   - https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-5.0
     * */
    public class BibleReadingDbContext : IdentityDbContext<BibleReader>
    {
        public BibleReadingDbContext(DbContextOptions<BibleReadingDbContext> options) : base(options)
        {
        }

        public BibleReadingDbContext()
        {
        }

        // BibleAggregate
        public DbSet<Language> Languages { get; set; }
        public DbSet<BibleVersion> BibleVersions { get; set; }
        public DbSet<BibleBook> BibleBooks { get; set; }
        public DbSet<BibleVerse> BibleVerses { get; set; }

        // BibleReaderAggregate
        public DbSet<Note> Notes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<TagGroup> TagGroups { get; set; }

        // Join Entities
        public DbSet<TagGroupNote> TagGroupNotes { get; set; }
        public DbSet<TagGroupTag> TagGroupTags { get; set; }
        public DbSet<TagNote> TagNotes { get; set; }
        public DbSet<NoteReference> NoteReferences { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<BibleReader>(b => b.ToTable("BibleReaders"));
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
