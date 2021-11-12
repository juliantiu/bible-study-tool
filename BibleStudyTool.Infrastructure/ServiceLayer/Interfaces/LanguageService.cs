using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer.Interfaces
{
    public class LanguageService : ILanguageService
    {
        private readonly LanguageQueries _languageQueries;

        public LanguageService(LanguageQueries languageQueries)
        {
            _languageQueries = languageQueries;
        }

        /// <summary>
        ///     Gets all application recognized languages.
        /// </summary>
        /// <returns>
        ///     The list of application-recognized languages.
        /// </returns>
        public async Task<IEnumerable<Language>> GetRecognizedLanguages()
        {
            return await _languageQueries.SelectAllLanguages();
        }
    }
}
