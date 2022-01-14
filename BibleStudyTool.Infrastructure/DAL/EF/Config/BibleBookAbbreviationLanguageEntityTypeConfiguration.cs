using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class BibleBookAbbreviationLanguageEntityTypeConfiguration : IEntityTypeConfiguration<BibleBookAbbreviationLanguage>
    {
        public void Configure(EntityTypeBuilder<BibleBookAbbreviationLanguage> builder)
        {
            builder.HasKey(bibleBookAbbreviationLanguage => new
            {
                bibleBookAbbreviationLanguage.BibleBookId,
                bibleBookAbbreviationLanguage.LanguageCode
            });

            builder.Property(bibleBookAbbreviationLanguage => bibleBookAbbreviationLanguage.Abbreviation)
                   .HasColumnName("BibleBookAbbreviation")
                   .IsRequired();

            builder.Property(bibleBookLanguage => bibleBookLanguage.Style)
                   .HasColumnName("BibleBookAbbreviationStyle")
                   .IsRequired();

            builder.HasOne<BibleBook>(bibleBookAbbreviationLanguage => bibleBookAbbreviationLanguage.BibleBook)
                   .WithMany(bibleBook => bibleBook.BibleBookAbbreviationLanguages)
                   .HasForeignKey(bibleVerseBibleVersionLanguage => bibleVerseBibleVersionLanguage.BibleBookId)
                   .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne<Language>(bibleBookAbbreviationLanguage => bibleBookAbbreviationLanguage.Language)
                   .WithMany(language => language.BibleBookAbbreviationLanguages)
                   .HasForeignKey(bibleBookAbbreviationLanguage => bibleBookAbbreviationLanguage.LanguageCode)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
