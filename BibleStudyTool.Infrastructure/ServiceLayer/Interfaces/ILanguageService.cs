using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ILanguageService
    {
        /// <summary>
        ///     Gets all application recognized languages.
        /// </summary>
        /// <returns>
        ///     The list of application-recognized languages.
        /// </returns>
        Task<IEnumerable<Language>> GetRecognizedLanguages();
    }
}
