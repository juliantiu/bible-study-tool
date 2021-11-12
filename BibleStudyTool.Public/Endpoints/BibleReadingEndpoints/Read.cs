using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Infrastructure.ServiceLayer;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleReadingEndpoints
{
    [Route("api/bible-reading/read")]
    [ApiController]
    public partial class Read : ControllerBase
    {
        private readonly ILanguageService _languageService;

        public Read(ILanguageService languageService)
        {
            _languageService = languageService;
        }

        [HttpGet("/language-options")]
        public async Task<ActionResult<IEnumerable<LanguageDto>>>
            GetLanguageOptions()
        {
            try
            {
                var languages = await _languageService.GetRecognizedLanguages();
                return Ok(languages.Select(l => new LanguageDto(l)));
            }
            catch (LanguageCrudActionException ex)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    new EntityCrudActionExceptionResponse()
                    { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Failed to get user chapter notes.");
            }
        }
    }
}
