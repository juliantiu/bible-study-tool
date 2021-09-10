using System;
using BibleStudyTool.Core.Entities.BibleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BookAbbreviationEntityTypeConfiguration: IEntityTypeConfiguration<BibleBookAbbreviation>
    {
        public void Configure(EntityTypeBuilder<BibleBookAbbreviation> builder)
        {
            builder.HasKey(bibleBookAbbreviation => bibleBookAbbreviation.Id);

            builder.Property(bibleBookAbbreviation => bibleBookAbbreviation.Abbreviation)
                   .IsRequired();

            builder.HasOne<BibleBook>(bibleBookAbbreviation => bibleBookAbbreviation.BibleBook)
                .WithMany(bibleBook => bibleBook.BibleBookAbbreviations)
                .HasForeignKey(bibleBookAbbreviation => bibleBookAbbreviation.BibleBookId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
