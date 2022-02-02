using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface IGroupedTagService
    {
        Task<GroupedTag> CreateGroupedTags(int tagGroupId, int tagId);
        Task RemoveGroupedTags(int tagGroupId, IEnumerable<int> tagIds);
    }
}
