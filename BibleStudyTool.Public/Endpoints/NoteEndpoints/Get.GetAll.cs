using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public partial class Get
    {
        [HttpGet("api/GetAllNotes")]
        public async Task<ActionResult<GetNotesResponse>> GetAllHandler()
        {
            try
            {
                var response = new GetNotesResponse();
                var result = await _itemRepository.GetAllAsync();
                response.Success = true;
                return response;
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get all notes.");
            }
        }
    }
}
