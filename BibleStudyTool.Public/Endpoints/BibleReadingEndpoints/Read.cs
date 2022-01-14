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
        private readonly IBibleBookLanguageService _bibleBookLanguageService;
        private readonly IBibleVersionLanguageService
            _bibleVersionLanguageService;
        private readonly ILanguageService _languageService;

        public Read
            (IBibleBookLanguageService bibleBookLanguageService,
            IBibleVersionLanguageService bibleVersionLanguageService,
            ILanguageService languageService)
        {
            _bibleBookLanguageService = bibleBookLanguageService;
            _bibleVersionLanguageService = bibleVersionLanguageService;
            _languageService = languageService;
        }

        [HttpGet("/languages")]
        public async Task<ActionResult<IEnumerable<LanguageDto>>>
            GetLanguageOptionsAsync()
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

        [HttpGet("/bible-version")]
        public async Task<ActionResult<IEnumerable<BibleVersionDto>>>
            GetBibleVersionOptionsAsync(string languageCode)
        {
            try
            {
                var versions =
                    await _bibleVersionLanguageService
                        .ListBibleVersions(languageCode);

                return Ok(versions.Select(v => new BibleVersionDto(v)));
            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Failed to get bible versions in the given language.");
            }
        }

        [HttpGet("/bible-books")]
        public async Task<ActionResult<IEnumerable<BibleBookDto>>>
            GetBibleBooksAsync(string languageCode, string style)
        {
            try
            {
                var bibleBooks =
                    await _bibleBookLanguageService.ListBibleBooks
                        (languageCode, style);

                return Ok(bibleBooks.Select(v => new BibleBookDto(v)));
            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Failed to get bible versions in the given language.");
            }
        }

        [HttpGet("/chapter")]
        public async Task<ActionResult<IEnumerable<ChapterVersesDto>>>
            GetChapter
                (string languageCode, int bibleVersionId, int chapterNumber)
        {
            var chapterText =
                await _bibleVerseBibleVersionLanguage
                    
        }

        [HttpGet("/verse")]
        public async Task<ActionResult<IEnumerable<ChapterVersesDto>>>
            GetVerse
                (string languageCode, int bibleVersionId, int bibleVerseId)
        {
            throw new NotImplementedException();
        }
    }
}
