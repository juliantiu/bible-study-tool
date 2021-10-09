using System;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleVerseBibleVersionLanguageEndpoints
{
    [Route("api/bible-verse-bible-version-language")]
    [ApiController]
    public partial class Get : ControllerBase
    {
        private readonly IAsyncRepository<BibleVerseBibleVersionLanguage> _bibleVerseBibleVersionLanguageRepository;

        public Get(IAsyncRepository<BibleVerseBibleVersionLanguage> bibleVerseBibleVersionLanguageRepository)
        {
            _bibleVerseBibleVersionLanguageRepository = bibleVerseBibleVersionLanguageRepository;
        }
    }
}
