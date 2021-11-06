using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ITagGroupService
    {
        Task<TagGroupWithTags> CreateTagGroupAsync(string uid, IEnumerable<int> tagIds);
    }
}
