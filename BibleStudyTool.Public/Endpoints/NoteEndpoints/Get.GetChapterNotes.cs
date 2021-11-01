using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public partial class Get
    {
        [HttpGet("get-chapter-notes")]
        public async Task<ActionResult<GetChapterNotesResponse>> GetChapterNotes
            (int bibleBookId, int chapterNumber)
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                var notes = await _noteService.GetAllChapterNotes(uid, bibleBookId, chapterNumber);
                return new GetChapterNotesResponse
                {
                    Notes = notes
                };
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get user chapter notes.");
            }
        }
    }
}
