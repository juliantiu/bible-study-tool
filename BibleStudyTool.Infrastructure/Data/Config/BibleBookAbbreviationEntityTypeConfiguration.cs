using System;
using BibleStudyTool.Core.Entities.BibleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleBookAbbreviationEntityTypeConfiguration: IEntityTypeConfiguration<BibleBookAbbreviation>
    {
        public void Configure(EntityTypeBuilder<BibleBookAbbreviation> builder)
        {
            builder.HasKey(bibleBookAbbreviation => bibleBookAbbreviation.BibleBookAbbreviationId);

            builder.Property(bibleBookAbbreviation => bibleBookAbbreviation.Abbreviation)
                   .HasMaxLength(10)
                   .HasColumnName("BibleBookAbbreviation")
                   .IsRequired();

            builder.HasOne<BibleBook>(bibleBookAbbreviation => bibleBookAbbreviation.BibleBook)
                .WithMany(bibleBook => bibleBook.BibleBookAbbreviations)
                .HasForeignKey(bibleBookAbbreviation => bibleBookAbbreviation.BibleBookId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
