using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public partial class Get
    {
        [HttpPost("api/GetAllUserNotes")]
        public async Task<ActionResult<GetNotesResponse>> GetAllUserNotesHandler()
        {
            throw new NotImplementedException();
        }
    }
}
