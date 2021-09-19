using System;
namespace BibleStudyTool.Core.NonEntityTypes
{
    public class WhereClause<T> : SpecificationClause where T : BaseEntity
    {
        public Func<T, bool> Value { get; set; }

        public WhereClause()
        {
        }
    }
}
