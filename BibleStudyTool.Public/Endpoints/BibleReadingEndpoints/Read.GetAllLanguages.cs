using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    public partial class Read
    {
        [HttpGet("/get-all-languages")]
        public async Task<ActionResult<GetAllLanguagesRespons>> GetAllLanguagesAsync()
        {
            return new GetAllLanguagesRespons
            {

            };
        }
    }
}
