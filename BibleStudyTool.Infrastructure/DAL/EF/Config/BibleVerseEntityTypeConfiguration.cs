using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.Config
{
    public class BibleVerseEntityTypeConfiguration : IEntityTypeConfiguration<BibleVerse>
    {
        public void Configure(EntityTypeBuilder<BibleVerse> builder)
        {
            builder.HasKey(bibleVerse => bibleVerse.BibleVerseId);

            builder.Property(bibleVerse => bibleVerse.ChapterNumber)
                   .IsRequired();

            builder.Property(bibleVerse => bibleVerse.VerseNumber)
                   .IsRequired();

            builder.Property(bibleVerse => bibleVerse.IsNewTestament)
                   .IsRequired();

            builder.HasOne<BibleBook>(bibleVerse => bibleVerse.BibleBook)
                   .WithMany(bibleBook => bibleBook.BibleVerses)
                   .HasForeignKey(bibleVerse => bibleVerse.BibleBookId)
                   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
