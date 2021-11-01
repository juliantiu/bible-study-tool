using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class TagService : ITagService
    {
        private readonly IAsyncRepository<Tag> _tagRepository;

        public TagService(IAsyncRepository<Tag> tagRepository)
        {
            _tagRepository = tagRepository;
        }

        public Task BulkCreateTag(IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }

        public async Task<Tag> CreateTagAsync(string uid, string label, string color)
        {
            var tagRef = new Tag(uid, label, color);
            return await _tagRepository.CreateAsync<TagCrudActionException>(tagRef);
        }

        public Task DeleteTag(int tagId)
        {
            throw new NotImplementedException();
        }

        public Task<Tag> UpdateTag(string uid, string Label, string color)
        {
            throw new NotImplementedException();
        }
    }
}
