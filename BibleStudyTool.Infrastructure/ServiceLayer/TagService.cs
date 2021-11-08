using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Exceptions;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="label"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public async Task<Tag> CreateTagAsync(string uid, string label, string color)
        {
            var tagRef = new Tag(uid, label, color);
            return await _tagRepository.CreateAsync<TagCrudActionException>(tagRef);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="newTags"></param>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="uid"></param>
        /// <param name="label"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public async Task<Tag> UpdateTagAsync(int tagId, string uid, string label, string color)
        {
            var tag = new Tag(tagId, uid, label, color);
            await _tagRepository.UpdateAsync<TagCrudActionException>(tag);
            return tag;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        public async Task DeleteTagAsync(string uid, int tagId)
        {
            Tag tag = await _tagRepository.GetByIdAsync<TagCrudActionException>(new object[1] { tagId });
            if (tag == null)
            {
                throw new Exception("The tag to be deleted does not exist.");
            }
            else if (tag.Uid != uid)
            {
                throw new UnauthorizedException("The logged in user does not own the tag to be deleted.");
            }
            await _tagRepository.DeleteAsync<TagCrudActionException>(tag);
        }
    }
}
