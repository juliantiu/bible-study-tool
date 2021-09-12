using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BibleStudyTool.Infrastructure.Data.Config
{
    public class TagGroupNoteEntityTypeConfiguration: IEntityTypeConfiguration<TagGroupNote>
    {
        public void Configure(EntityTypeBuilder<TagGroupNote> builder)
        {
            builder.HasKey(tagGroupNote => tagGroupNote.TagGroupNoteId);

            builder.HasOne<TagGroup>(tagGroupNote => tagGroupNote.TagGroup)
                   .WithMany(tagGroup => tagGroup.TagGroupNotes)
                   .HasForeignKey(tagGroupNote => tagGroupNote.TagGroupId)
                   .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Note>(tagGroupNote => tagGroupNote.Note)
                   .WithMany(note => note.TagGroupNotes)
                   .HasForeignKey(tagGroupNote => tagGroupNote.NoteId)
                   .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}
