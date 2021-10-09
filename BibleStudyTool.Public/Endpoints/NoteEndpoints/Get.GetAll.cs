using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public partial class Get
    {
        [HttpGet("all")]
        public async Task<ActionResult<GetAllUserNotesResponse>> GetAllHandler()
        {
            try
            {
                var response = new GetAllUserNotesResponse();
                var result = await _noteRepository.GetAllAsync<NoteCrudActionException>();
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
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get all notes.");
            }
        }
    }
}
