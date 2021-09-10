using System;
using BibleStudyTool.Core.Entities.BibleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleVerseEntityTypeConfiguration : IEntityTypeConfiguration<BibleVerse>
    {
        public void Configure(EntityTypeBuilder<BibleVerse> builder)
        {
            builder.HasKey(bibleVerse => bibleVerse.Id);

            builder.Property(bibleVerse => bibleVerse.ChapterNumber)
                   .IsRequired();

            builder.Property(bibleVerse => bibleVerse.VerseNumber)
                   .IsRequired();

            builder.Property(bibleVerse => bibleVerse.Text)
                    .IsRequired();

            builder.HasOne<BibleBook>(bibleVerse => bibleVerse.BibleBook)
                   .WithMany(bibleBook => bibleBook.BibleBookVerses)
                   .HasForeignKey(bibleVerse => bibleVerse.BibleBookId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
