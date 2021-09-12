using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class TagGroupTagEntityTypeConfiguration : IEntityTypeConfiguration<TagGroupTag>
    {
        public void Configure(EntityTypeBuilder<TagGroupTag> builder)
        {
            builder.HasKey(tagGroupTag => tagGroupTag.TagGroupTagId);

            builder.HasOne<TagGroup>(tagGroupTag => tagGroupTag.TagGroup)
                   .WithMany(tagGroup => tagGroup.TagGroupTags)
                   .HasForeignKey(tagGroupTag => tagGroupTag.TagGroupId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Tag>(tagGroupTag => tagGroupTag.Tag)
                   .WithMany(tag => tag.TagGroupTags)
                   .HasForeignKey(tagGroupTag => tagGroupTag.TagId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
