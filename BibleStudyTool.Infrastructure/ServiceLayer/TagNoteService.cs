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
        private readonly TagQueries _tagQueries;
        private readonly TagNoteQueries _tagNoteQueries;

        public TagNoteService(IAsyncRepository<TagNote> tagNoteRepository,
                              TagNoteQueries tagNoteQueries,
                              TagQueries tagQueries)
        {
            _tagNoteRepository = tagNoteRepository;
            _tagNoteQueries = tagNoteQueries;
            _tagQueries = tagQueries;
        }

        public async Task BulkCreateTagNotesAsync(int noteId, IEnumerable<int> tagIds)
        {
            var tagNotes = tagIds.Select(tagId => new TagNote(tagId, noteId)).ToArray();
            await _tagNoteRepository.BulkCreateAsync<TagNoteCrudActionException>(tagNotes);
        }

        public async Task<IDictionary<int, IList<Tag>>> GetTagsForNotesAsync(int[] noteIds)
        {
            return await _tagQueries.GetTagsForNotesQueryAsync(noteIds);
        }

        public async Task RemoveTagsFromNote(int noteId, IEnumerable<int> tagIdsToDelete)
        {
            await _tagNoteQueries.DeleteTagNotes(noteId, tagIdsToDelete);
        }
    }
}
