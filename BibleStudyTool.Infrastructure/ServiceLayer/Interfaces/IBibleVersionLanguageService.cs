using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface IBibleVersionLanguageService
    {
        /// <summary>
        ///     Lists all of the bible versions given the language code.
        /// </summary>
        /// <param name="languageCode"></param>
        /// <returns>
        ///     The list of bible versions supported by the language.
        /// </returns>
        Task<IEnumerable<(BibleVersion, BibleVersionLanguage)>>
            ListBibleVersions(string languageCode);
    }
}
