using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface IBibleVerseBibleVersionLanguageService
    {
        public Task<IEnumerable<BibleVerse>> GetChapterVerses
            (string languageCode, int bibleVersionId, int chapterNumber);
    }
}
