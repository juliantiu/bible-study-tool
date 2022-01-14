using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public partial class Delete
    {
        [HttpDelete("note")]
        [Authorize]
        public async Task<IActionResult> DeleteNoteAsync(int noteId)
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                await _noteService.DeleteAsync(uid, noteId);
                return Ok();

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
