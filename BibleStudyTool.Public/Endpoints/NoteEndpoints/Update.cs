using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [Route("api/note")]
    [ApiController]
    public class Update : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly UserManager<BibleReader> _userManager;

        public Update(INoteService noteService,
                      UserManager<BibleReader> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
        }

        [HttpPut("update")]
        [Authorize]
        public async Task<ActionResult<UpdateNoteResponse>> UpdateHandlerAsync(UpdateNoteRequest request)
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                var updatedNote = await _noteService.UpdateAsync(request.NoteId, uid, request.Summary, request.Text);
                var response = new UpdateNoteResponse();
                if (updatedNote is null)
                {
                    response.FailureMessage = "The note to be updated does not belong to the current logged in user.";
                }
                else
                {
                    response.Success = true;
                    response.Note = new NoteDto(updatedNote);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update note '{request.Summary}.'");
            }
        }
    }
}
