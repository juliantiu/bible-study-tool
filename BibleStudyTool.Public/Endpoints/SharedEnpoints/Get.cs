using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    [Route("api/shared")]
    [ApiController]
    public partial class Get : ControllerBase
    {
        private readonly IAsyncRepository<BibleBookAbbreviationLanguage> _bibleBookAbbreviationLanguageRepository;
        private readonly IAsyncRepository<BibleBookLanguage> _bibleBookLanguageRepository;
        private readonly IAsyncRepository<BibleVerse> _bibleVerseRepository;
        private readonly IAsyncRepository<BibleVerseBibleVersionLanguage> _bibleVerseBibleVersionLanguageRepository;
        private readonly IAsyncRepository<Note> _noteRepository;
        private readonly IAsyncRepository<NoteReference> _noteReferenceRepository;
        private readonly IAsyncRepository<Tag> _tagRepository;
        private readonly IAsyncRepository<TagGroup> _tagGroupRepository;

        private readonly UserManager<BibleReader> _userManager;


        public Get(IAsyncRepository<BibleBookAbbreviationLanguage> bibleBookAbbreviationLanguageRepository,
                   IAsyncRepository<BibleBookLanguage> bibleBookLanguageRepository,
                   IAsyncRepository<BibleVerse> bibleVerseRepository,
                   IAsyncRepository<BibleVerseBibleVersionLanguage> bibleVerseBibleVersionLanguageRepository,
                   IAsyncRepository<Note> noteRepository,
                   IAsyncRepository<NoteReference> noteReferenceRepository,
                   IAsyncRepository<Tag> tagRepository,
                   IAsyncRepository<TagGroup> tagGroupRepository,
                   UserManager<BibleReader> userManager)
        {
            _bibleBookAbbreviationLanguageRepository = bibleBookAbbreviationLanguageRepository;
            _bibleBookLanguageRepository = bibleBookLanguageRepository;
            _bibleVerseRepository = bibleVerseRepository;
            _bibleVerseBibleVersionLanguageRepository = bibleVerseBibleVersionLanguageRepository;
            _noteRepository = noteRepository;
            _noteReferenceRepository = noteReferenceRepository;
            _tagRepository = tagRepository;
            _tagGroupRepository = tagGroupRepository;
            _userManager = userManager;
        }
    }
}
