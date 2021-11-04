using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public partial class Read
    {
        [HttpGet("/get-all-bible-versions")]
        public async Task<ActionResult<GetAllBibleVersionsResponse>> GetAllBibleVersionsAsync()
        {
            return new GetAllBibleVersionsResponse
            {

            };
        }
    }
}
