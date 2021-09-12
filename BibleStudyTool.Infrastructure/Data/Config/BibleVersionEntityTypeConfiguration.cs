using System;
using BibleStudyTool.Core.Entities.BibleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class BibleVersionEntityTypeConfiguration: IEntityTypeConfiguration<BibleVersion>
    {
        public void Configure(EntityTypeBuilder<BibleVersion> builder)
        {
            builder.HasKey(bibleVersion => bibleVersion.BibleVersionId);

            builder.HasOne<Language>(bibleVersion => bibleVersion.Language)
                   .WithMany(language => language.BibleVersions)
                   .HasForeignKey(bibleVersion => bibleVersion.LanguageId)
                   .OnDelete(DeleteBehavior.ClientSetNull);

            builder.Property(bibleVersion => bibleVersion.Name)
                   .HasColumnName("BibleVersionName")
                   .HasMaxLength(150)
                   .IsRequired();

            builder.Property(bibleVersion => bibleVersion.Abbreviation)
                   .HasColumnName("BibleVersionAbbreviation")
                   .HasMaxLength(20);
        }
    }
}
