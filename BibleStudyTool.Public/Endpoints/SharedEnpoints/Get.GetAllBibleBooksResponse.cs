using System;
using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public class BibleBookItem
    {
        public int BibleBookId { get; set; } = 0;
        public string BibleBookName { get; set; } = string.Empty;
        public string BibleBookAbbreviation { get; set; } = string.Empty;
    }

    public class GetAllBibleBooksResponse : ApiResponseBase
    {
        public string LanguageCode { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public IList<BibleBookItem> BibleBookNamesAndAbbreviations { get; set; } = new List<BibleBookItem>();
    }
}
