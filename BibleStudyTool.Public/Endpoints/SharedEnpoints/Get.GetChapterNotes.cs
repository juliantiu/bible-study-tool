using System;
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
        [Authorize]
        [HttpGet("get-chapter-notes")]
        public async Task<ActionResult<GetChapterNotesResponse>> GetChapterNotesHandler(int bibleVersionId,
                                                                                        int bibleBookId,
                                                                                        int chapterNumber,
                                                                                        string languageCode)
        {
            try
            {
                var response = new GetChapterNotesResponse();
                var versesForChapter = await GetChapterVersesHandler(bibleVersionId, bibleBookId, chapterNumber, languageCode, _bibleVerseRepository, _bibleVerseBibleVersionLanguageRepository);
                var bibleVerseIds = versesForChapter.BibleVersesForChapter.Select(bvfc => bvfc.BibleVerseId).ToArray();

                var notesReferencesForChapter = await NoteReferenceEndpoints.Get.GetChapterNotesHandler(bibleVerseIds, _noteReferenceRepository);
                var noteIds = notesReferencesForChapter.NoteReferencesForChapter.Select(nr => nr.NoteId).ToArray();

                var userId = _userManager.GetUserId(User);
                var noteSpecRef = new Note(userId, string.Empty, string.Empty);
                var notesSpecification = new NoteFromNoteIdsSpecification(noteSpecRef, noteIds);
                var notes = await _noteRepository.GetBySpecification<NoteCrudActionException>(notesSpecification);
                foreach (var note in notes)
                {
                    response.ChapterNotes.Add(new NoteDto()
                    {
                        NoteId = note.NoteId,
                        Summary = note.Summary,
                        Text = note.Text,
                        Uid = note.Uid
                    });
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
