using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleBookLanguageEntityTypeConfiguration : IEntityTypeConfiguration<BibleBookLanguage>
    {
        public void Configure(EntityTypeBuilder<BibleBookLanguage> builder)
        {
            builder.HasKey(bibleBookLanguage => new
            {
                bibleBookLanguage.BibleBookId,
                bibleBookLanguage.LanguageCode
            });

            builder.Property(bibleBookLanguage => bibleBookLanguage.Name)
                   .HasColumnName("BibleBookName")
                   .IsRequired();

            builder.Property(bibleBookLanguage => bibleBookLanguage.Style)
                   .HasColumnName("BibleBookNameStyle")
                   .IsRequired();

            builder.HasOne<BibleBook>(bibleBookLanguage => bibleBookLanguage.BibleBook)
                   .WithMany(bibleBook => bibleBook.BibleBookLanguages)
                   .HasForeignKey(bibleBookLanguage => bibleBookLanguage.BibleBookId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.HasOne<Language>(bibleBookLanguage => bibleBookLanguage.Language)
                   .WithMany(language => language.BibleBookLanguages)
                   .HasForeignKey(bibleBookLanguage => bibleBookLanguage.LanguageCode)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
