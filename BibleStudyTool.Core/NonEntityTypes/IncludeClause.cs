using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Core.NonEntityTypes
{
    public class IncludeClause : SpecificationClause
    {
        public string PropertyName { get; set; }

        public IncludeClause()
        {
        }
    }
}
