using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.NonEntityTypes;

namespace BibleStudyTool.Core.Interfaces
{
    public interface ISpecification<T>
    {
        IList<SpecificationClause> SpecificationsClauses { get; set; }
    }
}
