using System;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.BibleBookAggregate.Exceptions;
using BibleStudyTool.Core.Entities.BibleBookAggregate.Specifications;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Entities.JoinEntities.Exceptions;
using BibleStudyTool.Core.Entities.JoinEntities.Specifications;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Public.Endpoints.SharedEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
	public partial class Get
	{
		[HttpGet("api/GetBibleVersesForChapter")]
		public async Task<ActionResult<GetBibleVersesForChapterResponse>> GetBibleVersesForChapterHandler(int bibleVersionId,
                                                                                                          int bibleBookId,
                                                                                                          int chapterNumber,
                                                                                                          string languageCode)
        {
            try
            {
                return Ok(await GetBibleVersesForChapterHandler(bibleVersionId,
                                                                bibleBookId,
                                                                chapterNumber,
                                                                languageCode,
                                                                _bibleVerseRepository,
                                                                _bibleVerseBibleVersionLanguageRepository));
            }
            catch (BibleVerseCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (BibleVerseBibleVersionLanguageCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  $"Failed to get bible verses for chapter {chapterNumber}");
            }
        }

        public static async Task<GetBibleVersesForChapterResponse> GetBibleVersesForChapterHandler(int bibleVersionId,
                                                                                                   int bibleBookId,
                                                                                                   int chapterNumber,
                                                                                                   string languageCode,
                                                                                                   IAsyncRepository<BibleVerse> bibleVerseRepository,
                                                                                                   IAsyncRepository<BibleVerseBibleVersionLanguage> bibleVerseBibleVersionLanguageRepository)
        {
            var response = new GetBibleVersesForChapterResponse();

            var bibleVerseSpecRef = new BibleVerse(chapterNumber, bibleBookId);
            var bibleVerseSpecification = new BibleVerseForChapterAndBookSpecification(bibleVerseSpecRef);
            var bibleVerses =
                await bibleVerseRepository.GetBySpecification<BibleVerseCrudActionException>(bibleVerseSpecification);

            var bibleVerseIds = bibleVerses.Select(b => b.BibleBookId);
            var bibleVerseBibleVersionLanguageSpecRef = new BibleVerseBibleVersionLanguage(bibleVersionId, languageCode);
            var bibleVerseBibleVersionLanguageSpecification = new BibleVerseTextForChapterAndBookSpecifications(bibleVerseBibleVersionLanguageSpecRef, bibleVerseIds);
            var bibleVerseBibleVersionLanguages =
                (await bibleVerseBibleVersionLanguageRepository.GetBySpecification<BibleVerseBibleVersionLanguageCrudActionException>(bibleVerseBibleVersionLanguageSpecification))
                                                               .ToDictionary(bvbvl => bvbvl.BibleVerseId,
                                                                             bvbvl => bvbvl.Text);

            foreach (var bibleVerse in bibleVerses)
            {
                var bibleTextFound = bibleVerseBibleVersionLanguages.TryGetValue(bibleVerse.BibleVerseId, out string text);

                response.BibleVersesForChapter.Add(new BibleVerseForChapterItem()
                {
                    BibleVerseId = bibleVerse.BibleVerseId,
                    ChapterNumber = chapterNumber,
                    VerseNumber = bibleVerse.VerseNumber,
                    IsNewTestament = bibleVerse.IsNewTestament,
                    Text = text
                });
            }
            response.Success = true;
            response.BibleBookId = bibleBookId;
            response.LanguageCode = languageCode;
            return response;
        }
    }
}
