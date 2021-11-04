using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public partial class Read
    {
        [HttpGet("/get-all-chapter-verses")]
        public async Task<ActionResult<GetAllChapterVersesResponse>> GetAllChapterVersesAsync
            (string languageCode, int bibleVersionId, int bibleBookId, int chapterNumber)
        {
            return new GetAllChapterVersesResponse
            {

            };
        }
    }
}
