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

        public async Task<Tag> CreateTagAsync(string uid, string label, string color)
        {
            var tagRef = new Tag(uid, label, color);
            return await _tagRepository.CreateAsync<TagCrudActionException>(tagRef);
        }

        public async Task<IEnumerable<Tag>> CreateTagsAsync(IEnumerable<Tag> newTags)
        {
            IList<Tag> tags = new List<Tag>();
            foreach (var newTag in newTags)
            {
                try
                {
                    tags.Add(await CreateTagAsync(newTag.Uid, newTag.Label, newTag.Color));
                }
                catch
                {
                    Console.WriteLine($"Failed to create tag '{newTag.Label}.'"); // TODO: log
                }
            }
            return tags;
        }
    }
}
