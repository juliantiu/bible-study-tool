using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class TagNoteService : ITagNoteService
    {
        private readonly IAsyncRepository<TagNote> _tagNoteRepository;
        private readonly TagNoteQueries _tagNoteQueries;

        public TagNoteService(IAsyncRepository<TagNote> tagNoteRepository, TagNoteQueries tagNoteQueries)
        {
            _tagNoteRepository = tagNoteRepository;
            _tagNoteQueries = tagNoteQueries;
        }

        public async Task BulkCreateTagNotesAsync(int noteId, IEnumerable<int> tagIds)
        {
            var tagNotes = tagIds.Select(tagId => new TagNote(tagId, noteId)).ToArray();
            await _tagNoteRepository.BulkCreateAsync<TagNoteCrudActionException>(tagNotes);
        }

        public async Task<IDictionary<int, IList<Tag>>> GetTagsForNotesAsync(int[] noteIds)
        {
            return await _tagNoteQueries.GetTagsForNotesQueryAsync(noteIds);
        }
    }
}
