using System;
namespace BibleStudyTool.Core.NonEntityTypes
{
    public struct EntityGetterRawSql
    {
        public string SqlCommand { get; set; }
        public string[] Parameters { get; set; }
    }
}
