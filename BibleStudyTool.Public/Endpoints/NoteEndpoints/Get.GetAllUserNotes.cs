using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Entities.Specifications;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public partial class Get
    {
        [Authorize]
        [HttpPost("all-user-notes-with-tags-and-references")]
        public async Task<ActionResult<GetAllUserNotesResponse>> GetAllUserNotesHandler()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                var response = await GetAllUserNotesHandler(userId, _noteRepository);
                return response;
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get all user notes with their tags and references");
            }
        }

        public async Task<ActionResult<GetAllUserNotesResponse>> GetAllUserNotesHandler(string uid,
                                                                                                                                  IAsyncRepository<Note> noteRepository)
        {
            var response = new GetAllUserNotesResponse();
            var noteSpecRef = new Note(uid, string.Empty, string.Empty);
            var noteSpecification = new NoteWithAllTagsAndReferencesSpecification(noteSpecRef);
            var notes = await noteRepository.GetBySpecification<NoteCrudActionException>(noteSpecification);
            foreach (var note in notes)
            {
                response.Notes.Add(new NoteDto()
                {
                    NoteId = note.NoteId,
                    Summary = note.Summary,
                    Text = note.Text,
                    Uid = uid
                });
            }
            response.Success = true;
            response.Uid = uid;
            return response;
        }
    }
}
