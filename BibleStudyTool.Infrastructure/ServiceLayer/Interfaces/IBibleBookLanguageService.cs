using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface IBibleBookLanguageService
    {
        /// <summary>
        ///     Gets the bible book names of a given language and formatting
        ///     style.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <param name="style"></param>
        /// <returns>
        /// The list of bible book names.
        /// </returns>
        Task<IEnumerable
            <(BibleBook, BibleBookLanguage, BibleBookAbbreviationLanguage)>>
                ListBibleBooks
                    (string languageCode, string style);
    }
}
