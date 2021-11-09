using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.ServiceLayer;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class TagGroupTagService : ITagGroupTagService
    {
        private readonly IAsyncRepository<TagGroupTag> _tagGroupTagRepository;

        public TagGroupTagService(IAsyncRepository<TagGroupTag> tagGroupTagRepository)
        {
            _tagGroupTagRepository = tagGroupTagRepository;
        }

        public async Task<TagGroupTag> CreateTagGroupTag(int tagGroupId, int tagId)
        {
            var tagGroupTag = new TagGroupTag(tagGroupId, tagId);
            return await _tagGroupTagRepository.CreateAsync<TagGroupTagCrudActionException>(tagGroupTag);
        }

        public async Task RemoveTagsFromTagGroup(int tagGroupId, IEnumerable<int> tagIds)
        {
            var tagGroupTagKeys = new List<object[]>();
            foreach (var tagId in tagIds)
            {
                tagGroupTagKeys.Add(new object[2] { tagGroupId, tagId });
            }

            await _tagGroupTagRepository.BulkDeleteAsync<TagGroupTagCrudActionException>(tagGroupTagKeys.ToArray());
        }
    }
}
