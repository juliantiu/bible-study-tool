using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public partial class Get
    {
        [HttpPost("api/GetAllUserNotesForChapter")]
        public async Task<ActionResult<GetAllUserNotesWithTagsAndReferencesResponse>> GetAllUserNotesForChapterHandler()
        {
            throw new NotImplementedException();
        }
    }
}
