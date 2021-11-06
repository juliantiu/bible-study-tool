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
    }
}
