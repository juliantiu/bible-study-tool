using System;
using BibleStudyTool.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.DAL.EF.Config
{
    public class GroupedTagEntityTypeConfiguration : IEntityTypeConfiguration<GroupedTag>
    {
        public void Configure(EntityTypeBuilder<GroupedTag> builder)
        {
            builder.HasKey(tagGroupTag => new { tagGroupTag.TagGroupId, tagGroupTag.TagId });

            builder.HasOne(groupedTag => groupedTag.TagGroup)
                   .WithMany(tagGroup => tagGroup.GroupedTags)
                   .HasForeignKey(groupedTag => groupedTag.TagGroupId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(groupedTag => groupedTag.Tag)
                   .WithMany(tag => tag.GroupedTags)
                   .HasForeignKey(groupedTag => groupedTag.TagId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
