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
            builder.HasKey(tagGroup => tagGroup.TagGroupId);

            builder.Property(tagGroup => tagGroup.Uid)
                   .HasColumnName("TagGroupUid")
                   .IsRequired();

            builder.Property(tagGroup => tagGroup.Label)
                   .HasColumnName("TagGroupLabel")
                   .HasMaxLength(100)
                   .IsRequired();
        }
    }
}
