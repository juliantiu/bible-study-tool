using BibleStudyTool.Core.Entities.BibleVerse;
using BibleStudyTool.Infrastructure.ServiceLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class BibleVerseService : IBibleVerseService
    {
        public Task<IEnumerable<BibleVerse>> GetChapterVerses(string language, string version, string bookKey, int chapter)
        {
            throw new NotImplementedException();
        }

        public Task<BibleVerse> GetVerse(string verseReferenceKey, string language, string version)
        {
            throw new NotImplementedException();
        }
    }
}
