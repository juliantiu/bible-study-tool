using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class BibleBookLanguageService : IBibleBookLanguageService
    {
        private readonly BibleBookLanguageQueries _bibleBookLanguageQueries;

        public BibleBookLanguageService
            (BibleBookLanguageQueries bibleBookLanguageQueries)
        {
            _bibleBookLanguageQueries = bibleBookLanguageQueries;
        }

        /// <summary>
        ///     Gets the bible book names of a given language and formatting
        ///     style.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="style"></param>
        /// <returns>
        /// The list of bible book names.
        /// </returns>
        public async Task
            <IEnumerable
                <(BibleBook, BibleBookLanguage, BibleBookAbbreviationLanguage)>>
                ListBibleBooks
                    (string languageCode, string style)
        {
            return await _bibleBookLanguageQueries.SelectBibleBooks
                (languageCode, style);
        }
    }
}
