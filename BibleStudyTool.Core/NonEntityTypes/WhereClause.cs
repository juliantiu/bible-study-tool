using System;
using System.Linq.Expressions;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.NonEntityTypes
{
    public class WhereClause<T> : SpecificationClause where T : BaseEntity
    {
        public Expression<Func<T, bool>> Expression { get; set; }

        public WhereClause()
        {
        }
    }
}
