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
    [ApiController]
    public class Update : ControllerBase
    {
        private readonly IAsyncRepository<Note> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Update(IAsyncRepository<Note> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpPut("api/tag")]
        [Authorize]
        public async Task<ActionResult<UpdateNoteResponse>> UpdateHandler(UpdateNoteRequest request)
        {
            var response = new UpdateNoteResponse();
            try
            {
                var currentUserId = _userManager.GetUserId(User);
                var note = await _itemRepository.GetByIdAsync<NoteCrudActionException>(request.NoteId);
                if (note.Uid != currentUserId)
                {
                    response.FailureMessage = "The current user does not own the note being updated.";
                    return response;
                }
                note.UpdateDetails(request.Summary, request.Text);
                await _itemRepository.UpdateAsync<NoteCrudActionException>(note);
                response.Success = true;
                return response;
            }
            catch (NoteCrudActionException ncaex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ncaex.Timestamp, Message = ncaex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update note '{request.Summary}.'");
            }
        }
    }
}
