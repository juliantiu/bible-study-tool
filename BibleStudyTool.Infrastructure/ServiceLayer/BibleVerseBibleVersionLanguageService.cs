using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class BibleVerseBibleVersionLanguageService
        : IBibleVerseBibleVersionLanguageService
    {
        public BibleVerseBibleVersionLanguageService()
        {
        }

        public Task<IEnumerable<BibleVerse>> GetChapterVerses(string languageCode, int bibleVersionId, int chapterNumber)
        {
            throw new NotImplementedException();
        }
    }
}
