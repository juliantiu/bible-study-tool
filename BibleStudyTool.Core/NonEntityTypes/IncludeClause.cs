using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.NonEntityTypes
{
    public class IncludeClause<T> : SpecificationClause where T : BaseEntity
    {
        public string PropertyName { get; set; } = string.Empty;

        public IncludeClause()
        {
        }
    }
}
