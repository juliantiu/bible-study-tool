using System;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.NonEntityTypes
{
    public class WhereClause<T> : SpecificationClause where T : BaseEntity
    {
        public Func<T, bool> Expression { get; set; }

        public WhereClause()
        {
        }
    }
}
