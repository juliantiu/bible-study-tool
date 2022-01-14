using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class BibleVersionLanguageService : IBibleVersionLanguageService
    {
        private readonly BibleVersionLanguageQueries
            _bibleVersionLanguageQueries;

        public BibleVersionLanguageService
            (BibleVersionLanguageQueries bibleVersionLanguageQueries)
        {
            _bibleVersionLanguageQueries = bibleVersionLanguageQueries;
        }

        /// <summary>
        ///     Lists all of the bible versions given the language code.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns>
        ///     The list of bible versions supported by the language.
        /// </returns>
        public async Task<IEnumerable<(BibleVersion, BibleVersionLanguage)>>
            ListBibleVersions(string languageCode)
        {
            return await _bibleVersionLanguageQueries
                .SelectBibleVersionInLanguag(languageCode);
        }
    }
}
