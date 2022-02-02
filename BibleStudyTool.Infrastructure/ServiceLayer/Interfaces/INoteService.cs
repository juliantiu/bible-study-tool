using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface INoteService
    {
        /// <summary>
        ///     Creates a new note.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="summary"></param>
        /// <param name="text"></param>
        /// <param name="bibleVerseReferences"></param>
        /// <param name="noteReferences"></param>
        /// <param name="existingTags"></param>
        /// <param name="newTags"></param>
        /// <returns>
        ///     The created note along with its tags and note/verse references.
        /// </returns>
        Task<NoteWithTagsAndReferences> CreateAsync
            (string uid, string summary, string text, IEnumerable<NoteVerseReference> noteVerseReferences,
            IEnumerable<int> noteReferences, IEnumerable<int> existingTags, IEnumerable<Tag> newTags);

        /// <summary>
        ///     Deletes a note.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="noteId"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// <exception cref="UnauthorizedException"></exception>
        Task DeleteAsync(string uid, int noteId);

        /// <summary>
        ///     Gets all of the notes associated with an entire bible chapter.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="bibleBookId"></param>
        /// <param name="chapterNumber"></param>
        /// <returns>
        ///     A list of notes associated with a bible chapter along with its
        ///     tags and note/verse references.
        /// </returns>
        Task<IEnumerable<NoteWithTagsAndReferences>> GetAllChapterNotesAsync(string uid, int bibleBookId, int chapterNumber);

        /// <summary>
        ///     Updates a note.
        /// </summary>
        /// <param name="noteId"></param>
        /// <param name="uid"></param>
        /// <param name="summary"></param>
        /// <param name="text"></param>
        /// <param name="tagIds"></param>
        /// <param name="noteVerseReferences"></param>
        /// <param name="noteReferenceIds"></param>
        /// <param name="newTags"></param>
        /// <returns>
        ///     The updated note along with its note/verse references.
        /// </returns>
        /// <exception cref="UnauthorizedException"></exception>
        Task<NoteWithTagsAndReferences> UpdateAsync
            (int noteId, string uid, string summary, string text,
            IEnumerable<int> tagIds, IEnumerable<NoteVerseReference> noteVerseReferences,
            IEnumerable<int> noteReferenceIds, IEnumerable<Tag> newTags);
    }
}
