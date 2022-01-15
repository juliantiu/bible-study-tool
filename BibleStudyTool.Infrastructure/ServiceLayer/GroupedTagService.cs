using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class GroupedTagService : IGroupedTagService
    {
        private readonly IAsyncRepository<GroupedTag> _tagGroupTagRepository;

        public GroupedTagService(IAsyncRepository<GroupedTag> tagGroupTagRepository)
        {
            _tagGroupTagRepository = tagGroupTagRepository;
        }

        public async Task<GroupedTag> CreateTagGroupTag(int tagGroupId, int tagId)
        {
            var tagGroupTag = new GroupedTag(tagGroupId, tagId);
            return await _tagGroupTagRepository.CreateAsync<EntityCrudActionException>(tagGroupTag);
        }

        public async Task RemoveTagsFromTagGroup(int tagGroupId, IEnumerable<int> tagIds)
        {
            var tagGroupTagKeys = new List<object[]>();
            foreach (var tagId in tagIds)
            {
                tagGroupTagKeys.Add(new object[2] { tagGroupId, tagId });
            }

            await _tagGroupTagRepository.BulkDeleteAsync<EntityCrudActionException>(tagGroupTagKeys.ToArray());
        }
    }
}
