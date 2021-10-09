using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [Route("api/note")]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<Note> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<Note> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpDelete("delete")]
        [Authorize]
        public async Task<ActionResult<DeleteNoteResponse>> DeleteHandler(string id)
        {
            try
            {
                var response = new DeleteNoteResponse();
                var currentUserId = _userManager.GetUserId(User);
                var keyId = new object[] { id }; 
                var note = await _itemRepository.GetByIdAsync<NoteCrudActionException>(keyId);
                if (note.Uid != currentUserId)
                {
                    response.FailureMessage = "The current user does not own the note being deleted.";
                    return response;
                }
                await _itemRepository.DeleteAsync<NoteCrudActionException>(note);
                response.Success = true;
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
