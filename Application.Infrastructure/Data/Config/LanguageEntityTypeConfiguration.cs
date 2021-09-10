using System;
using BibleStudyTool.Core.Entities.BibleAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class LanguageEntityTypeConfiguration: IEntityTypeConfiguration<Language>
    {
        public void Configure(EntityTypeBuilder<Language> builder)
        {
            builder.HasKey(language => language.Id);

            builder.Property(language => language.Name)
                   .HasMaxLength(70)
                   .IsRequired();

            builder.Property(language => language.Code)
                   .HasMaxLength(3); // ISO 639-3 codes

            builder.Property(language => language.Endonym)
                   .HasMaxLength(70);
        }
    }
}
