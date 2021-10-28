using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.BibleBookAggregate.Exceptions;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Entities.JoinEntities.Exceptions;
using BibleStudyTool.Core.Entities.Specifications;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public partial class Get
    {
        [HttpGet("get-chapter-notes")]
        public async Task<ActionResult<GetChapterNotesResponse>> GetChapterNotesHandler(int bibleVersionId,
                                                                                        int bibleBookId,
                                                                                        int chapterNumber,
                                                                                        string languageCode)
        {
            try
            {
                // Gets the bible verses for a given chapter
                var response = new GetChapterNotesResponse();
                var versesForChapter = await GetChapterVersesHandler(bibleVersionId, bibleBookId, chapterNumber, languageCode, _bibleVerseRepository, _bibleVerseBibleVersionLanguageRepository);
                var bibleVerseIds = versesForChapter.BibleVersesForChapter.Select(bvfc => bvfc.BibleVerseId).ToArray();

                // Gets all the notes that references the chapter
                var notesReferencesForChapter = await NoteReferenceEndpoints.Get.GetChapterNotesHandler(bibleVerseIds, _noteReferenceRepository);
                var noteReferencesIds = notesReferencesForChapter.NoteReferencesForChapter.Select(noteRef => noteRef.NoteId).ToHashSet().ToArray();

                // Gets all the notes that references the resulting notes from above
                var notesReferencesForNotes = await NoteReferenceEndpoints.Get.GetNotesThatReferencesSpecifiedNotesHandler(noteReferencesIds, _noteReferenceRepository);

                var noteIdsToReferencesMapping = new Dictionary<int, NoteReferencesContainer>();
                var noteIds = new HashSet<int>();

                // Record note references that reference the given chapter in the lookup table
                foreach (var noteReference in notesReferencesForChapter.NoteReferencesForChapter)
                {
                    noteIds.Add(noteReference.NoteId);
                    if (noteIdsToReferencesMapping.TryGetValue(noteReference.NoteId, out var noteReferenceContainer))
                    {
                        if (noteReference.ReferencedBibleVerseId is int rbvid)
                            noteReferenceContainer.ReferencedBibleVerses.Add(rbvid);
                    }
                    else
                    {
                        var noteReferencesContainer = new NoteReferencesContainer();
                        if (noteReference.ReferencedBibleVerseId is int rbvid)
                            noteReferencesContainer.ReferencedBibleVerses.Add(rbvid);
                        noteIdsToReferencesMapping.Add(noteReference.NoteId, noteReferencesContainer);
                    }
                }

                // Record the note references that references the notes that references the given chapter in the lookup table
                foreach (var noteReference in notesReferencesForNotes.NoteReferencesForChapter)
                {
                    noteIds.Add(noteReference.NoteId);
                    if (noteIdsToReferencesMapping.TryGetValue(noteReference.NoteId, out var noteReferenceContainer))
                    {
                        if (noteReference.ReferencedNoteId is int rnid)
                            noteReferenceContainer.ReferencedNotes.Add(rnid);
                    }
                    else
                    {
                        var noteReferencesContainer = new NoteReferencesContainer();
                        if (noteReference.ReferencedNoteId is int rnid)
                            noteReferencesContainer.ReferencedNotes.Add(rnid);
                        noteIdsToReferencesMapping.Add(noteReference.NoteId, noteReferencesContainer);
                    }
                }

                var userId = _userManager.GetUserId(User);
                var noteSpecRef = new Note(userId, string.Empty, string.Empty);
                var notesSpecification = new NoteFromNoteIdsSpecification(noteSpecRef, noteIds);
                var notes = await _noteRepository.GetBySpecification<NoteCrudActionException>(notesSpecification);
                foreach (var note in notes)
                {
                    var foundRefContainer = noteIdsToReferencesMapping.TryGetValue(note.NoteId, out var referenceContainer);
                    response.ChapterNotes.Add(new NoteDto(note, foundRefContainer ? referenceContainer : new NoteReferencesContainer() ));
                }
                response.Success = true;
                return response;
            }
            catch (BibleVerseCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (BibleVerseBibleVersionLanguageCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (NoteReferenceCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get note reference(s) for the given bible book chapter.");
            }
        }
    }
}
