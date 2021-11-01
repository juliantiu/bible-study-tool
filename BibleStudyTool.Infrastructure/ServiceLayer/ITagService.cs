using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ITagService
    {
        Task BulkCreateTag(IEnumerable<Tag> tags);
        Task<Tag> CreateTagAsync(string uid, string label, string color);
        Task DeleteTag(int tagId);
        Task<Tag> UpdateTag(string uid, string Label, string color); 
    }
}
