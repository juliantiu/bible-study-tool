using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [Route("api/note")]
    public class Delete : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(INoteService noteService,
                      UserManager<BibleReader> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteNoteResponse>> DeleteHandler(int noteId)
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                var deletedNote = await _noteService.DeleteAsync(uid, noteId);
                var response = new DeleteNoteResponse();
                if (!deletedNote)
                {
                    response.FailureMessage = "The note to be deleted does not belong to the current logged in user.";
                }
                else
                {
                    response.Success = true;
                }
                return response;

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
    }
}
