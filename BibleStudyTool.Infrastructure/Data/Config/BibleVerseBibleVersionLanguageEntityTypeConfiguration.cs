using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleVerseBibleVersionLanguageEntityTypeConfiguration : IEntityTypeConfiguration<BibleVerseBibleVersionLanguage>
    {
        public void Configure(EntityTypeBuilder<BibleVerseBibleVersionLanguage> builder)
        {
            builder.HasKey(bibleVerseBibleVersionLanguage => new
            {
                bibleVerseBibleVersionLanguage.BibleVerseId,
                bibleVerseBibleVersionLanguage.BibleVersionId,
                bibleVerseBibleVersionLanguage.LanguageCode
            });

            builder.Property(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.Text)
                   .HasColumnName("BibleVerseBibleVersionLanguageText")
                   .IsRequired();

            builder.HasOne<BibleVerse>(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.BibleVerse)
                   .WithMany(bibleVerse => bibleVerse.BibleVerseBibleVersionLanguages)
                   .HasForeignKey(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.BibleVerseId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<BibleVersion>(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.BibleVersion)
                   .WithMany(bibleVersion => bibleVersion.BibleVerseBibleVersionLanguages)
                   .HasForeignKey(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.BibleVersionId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Language>(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.Language)
                   .WithMany(language => language.BibleVerseBibleVersionLanguages)
                   .HasForeignKey(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.LanguageCode)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
