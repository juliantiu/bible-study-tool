using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class TagGroupEntityTypeConfiguration: IEntityTypeConfiguration<TagGroup>
    {
        public void Configure(EntityTypeBuilder<TagGroup> builder)
        {
            builder.HasKey(tagGroup => tagGroup.Id);

            builder.Property(tagGroup => tagGroup.Uid)
                   .IsRequired();

            builder.Property(tagGroup => tagGroup.Label)
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
