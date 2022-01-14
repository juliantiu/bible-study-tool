using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class BibleVersionLanguageEntityTypeConfiguration : IEntityTypeConfiguration<BibleVersionLanguage>
    {
        public void Configure(EntityTypeBuilder<BibleVersionLanguage> builder)
        {
            builder.HasKey(bibleVersionLanguage => new { bibleVersionLanguage.BibleVersionId, bibleVersionLanguage.LanguageCode });

            builder.Property(bibleVersionLanguage => bibleVersionLanguage.Name)
                   .HasColumnName("BibleVersionName")
                   .IsRequired();

            builder.Property(bibleVersionLanguage => bibleVersionLanguage.Abbreviation)
                   .HasColumnName("BibleVersionAbbreviation");

            builder.HasOne<BibleVersion>(bibleVersionLanguage => bibleVersionLanguage.BibleVersion)
                   .WithMany(bibleVersion => bibleVersion.BibleVersionLanguages)
                   .HasForeignKey(bibleVersionLanguage => bibleVersionLanguage.BibleVersionId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Language>(bibleVersionLanguage => bibleVersionLanguage.Language)
                   .WithMany(language => language.BibleVersionLanguages)
                   .HasForeignKey(bibleVersionLanguage => bibleVersionLanguage.LanguageCode)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
