﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ITagService
    {
        Task<IEnumerable<Tag>> CreateTagsAsync(IEnumerable<Tag> newTags);
        Task<Tag> CreateTagAsync(string uid, string label, string color);
        Task<Tag> UpdateTagAsync(int tagId, string uid, string label, string color);
        Task DeleteTagAsync(string uid, int tagId);
    }
}
