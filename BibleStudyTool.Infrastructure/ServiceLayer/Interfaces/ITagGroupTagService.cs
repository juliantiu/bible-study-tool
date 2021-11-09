using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ITagGroupTagService
    {
        Task<TagGroupTag> CreateTagGroupTag(int tagGroupId, int tagId);
        Task RemoveTagsFromTagGroup(int tagGroupId, IEnumerable<int> tagIds)
    }
}
