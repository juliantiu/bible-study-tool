using System;
using System.Collections.Generic;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public class BibleBookNamesAndAbbreviationsItem
    {
        public int BibleBookId { get; set; } = 0;
        public string BibleBookName { get; set; } = string.Empty;
        public string BibleBookAbbreviation { get; set; } = string.Empty;
    }

    public class GetAllBibleBookNamesAndAbbreviationsResponse : ApiResponseBase
    {
        public string LanguageCode { get; set; } = string.Empty;
        public string Style { get; set; } = string.Empty;
        public IList<BibleBookNamesAndAbbreviationsItem> BibleBookNamesAndAbbreviations { get; set; } = new List<BibleBookNamesAndAbbreviationsItem>();
    }
}
