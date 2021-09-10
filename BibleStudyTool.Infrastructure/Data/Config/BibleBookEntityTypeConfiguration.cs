using System;
using BibleStudyTool.Core.Entities.BibleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleBookEntityTypeConfiguration: IEntityTypeConfiguration<BibleBook>
    {
        public void Configure(EntityTypeBuilder<BibleBook> builder)
        {
            builder.HasKey(bibleBook => bibleBook.Id);

            builder.Property(bibleBook => bibleBook.Name)
                   .HasMaxLength(30);

            builder.HasOne<BibleVersion>(bibleBook => bibleBook.BibleVersion)
                   .WithMany(BibleVersion => BibleVersion.BibleBooks)
                   .HasForeignKey(bibleBook => bibleBook.BibleVersionId)
                   .OnDelete(DeleteBehavior.ClientSetNull);
        }
    }
}
