using System;
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

        public Get(IAsyncRepository<BibleBookAbbreviationLanguage> bibleBookAbbreviationLanguageRepository,
                   IAsyncRepository<BibleBookLanguage> bibleBookLanguageRepository)
        {
            _bibleBookAbbreviationLanguageRepository = bibleBookAbbreviationLanguageRepository;
            _bibleBookLanguageRepository = bibleBookLanguageRepository;
        }
    }
}
