using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    [ApiController]
    public partial class Get : ControllerBase
    {
        private readonly IAsyncRepository<BibleBookAbbreviationLanguage> _bibleBookAbbreviationLanguageRepository;
        private readonly IAsyncRepository<BibleBookLanguage> _bibleBookLanguageRepository;
        private readonly IAsyncRepository<BibleVerse> _bibleVerseRepository;
        private readonly IAsyncRepository<BibleVerseBibleVersionLanguage> _bibleVerseBibleVersionLanguageRepository;

        public Get(IAsyncRepository<BibleBookAbbreviationLanguage> bibleBookAbbreviationLanguageRepository,
                   IAsyncRepository<BibleBookLanguage> bibleBookLanguageRepository,
                   IAsyncRepository<BibleVerse> bibleVerseRepository,
                   IAsyncRepository<BibleVerseBibleVersionLanguage> bibleVerseBibleVersionLanguageRepository)
        {
            _bibleBookAbbreviationLanguageRepository = bibleBookAbbreviationLanguageRepository;
            _bibleBookLanguageRepository = bibleBookLanguageRepository;
            _bibleVerseRepository = bibleVerseRepository;
            _bibleVerseBibleVersionLanguageRepository = bibleVerseBibleVersionLanguageRepository;
        }
    }
}
