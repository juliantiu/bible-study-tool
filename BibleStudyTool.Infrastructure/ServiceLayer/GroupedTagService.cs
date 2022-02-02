using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class GroupedTagService : IGroupedTagService
    {
        private readonly IAsyncRepository<GroupedTag> _groupedTagRepository;

        public GroupedTagService(IAsyncRepository<GroupedTag> tagGroupTagRepository)
        {
            _groupedTagRepository = tagGroupTagRepository;
        }

        public async Task<GroupedTag> CreateGroupedTags(int tagGroupId, int tagId)
        {
            var tagGroupTag = new GroupedTag(tagGroupId, tagId);
            return await _groupedTagRepository.CreateAsync(tagGroupTag);
        }

        public async Task RemoveGroupedTags(int tagGroupId, IEnumerable<int> tagIds)
        {
            var groupedTagKeys =
                tagIds.Select(tagId => new object[2] { tagGroupId, tagId});

            await _groupedTagRepository.BulkDeleteAsync(groupedTagKeys.ToArray());
        }
    }
}
