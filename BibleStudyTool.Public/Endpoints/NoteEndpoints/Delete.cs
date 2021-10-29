using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [Route("api/note")]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<Note> _noteRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<Note> noteRepository,
                      UserManager<BibleReader> userManager)
        {
            _noteRepository = noteRepository;
            _userManager = userManager;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteNoteResponse>> DeleteHandler(int id)
        {
            try
            {
                string userId = _userManager.GetUserId(User);
                return Ok(await DeleteHandler(id, userId, _noteRepository));

            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete note.");
            }
        }

        public static async Task<DeleteNoteResponse> DeleteHandler(int noteId, string userId, IAsyncRepository<Note> noteRepository)
        {
            var response = new DeleteNoteResponse();
            var keyId = new object[] { noteId };
            var note = await noteRepository.GetByIdAsync<NoteCrudActionException>(keyId);
            if (note.Uid != userId)
            {
                response.FailureMessage = "The current user does not own the note being deleted.";
                return response;
            }
            await noteRepository.DeleteAsync<NoteCrudActionException>(note);
            response.Success = true;
            return response;
        }
    }
}
