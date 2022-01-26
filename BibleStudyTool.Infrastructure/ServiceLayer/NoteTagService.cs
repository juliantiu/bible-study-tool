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
    public class NoteTagService : INoteTagService
    {
        private readonly IAsyncRepository<NoteTag> _noteTagRepository;
        private readonly TagQueries _tagQueries;
        private readonly NoteTagQueries _noteTagQueries;

        public NoteTagService(IAsyncRepository<NoteTag> noteTagRepository,
                              NoteTagQueries noteTagQueries,
                              TagQueries tagQueries)
        {
            _noteTagRepository = noteTagRepository;
            _noteTagQueries = noteTagQueries;
            _tagQueries = tagQueries;
        }

        public async Task BulkCreateNoteTagsAsync(int noteId, IEnumerable<int> tagIds)
        {
            var tagNotes = tagIds.Select(tagId => new NoteTag(tagId, noteId)).ToArray();
            await _noteTagRepository.BulkCreateAsync<TagNoteCrudActionException>(tagNotes);
        }

        public async Task<IDictionary<int, IList<Tag>>> GetTagsForNotesAsync(int[] noteIds)
        {
            return await _tagQueries.GetTagIdsForNotesQueryAsync(noteIds);
        }

        public async Task RemoveTagsFromNote(int noteId, IEnumerable<int> tagIdsToDelete)
        {
            await _noteTagQueries.DeleteTagNotes(noteId, tagIdsToDelete);
        }
    }
}
