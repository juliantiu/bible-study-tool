using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public partial class Read
    {
        [HttpGet("/get-all-bible-books")]
        public async Task<ActionResult<GetAllBibleBooksResponse>> GetAllBibleBooksAsync
            (string languageCode, int bibleVersionId, string style)
        {
            return new GetAllBibleBooksResponse
            {

            };
        }
    }
}
