using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.DAL.Npgsql;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public class TagGroupService : ITagGroupService
    {
        private readonly IAsyncRepository<TagGroup> _tagGroupRepository;
        private readonly ITagGroupTagService _tagGroupTagService;
        private readonly TagQueries _tagQueries;

        public TagGroupService(IAsyncRepository<TagGroup> tagGroupRepository, ITagGroupTagService tagGroupTagService, TagQueries tagQueries)
        {
            _tagGroupRepository = tagGroupRepository;
            _tagGroupTagService = tagGroupTagService;
            _tagQueries = tagQueries;
        }

        public async Task<TagGroupWithTags> CreateTagGroupAsync(string uid, IEnumerable<int> tagIds)
        {
            if (tagIds.Count() < 2)
            {
                return null;
            }

            // Create the new tag group
            var tagGroup = new TagGroup(uid);
            var newTagGroup = await _tagGroupRepository.CreateAsync<TagGroupCrudActionException>(tagGroup);
            var tagGroupId = newTagGroup.TagGroupId;

            // Associate the input tag IDs with the newly created tag group
            var tagGroupTags = await associateTagsWithTagGroup(tagGroupId, tagIds);
            newTagGroup.AssignTags(tagGroupTags);

            // Get tags with the tag IDS associated to the tag group
            var tags = await _tagQueries.GetTagsInTagGroupAsync(tagGroupId);

            return new TagGroupWithTags(newTagGroup, tags);
        }

        #region************************************************** HELPER METHODS
        // *********************************************************************
        // *********************************************************************

        private async Task<IEnumerable<TagGroupTag>> associateTagsWithTagGroup(int tagGroupId, IEnumerable<int> tagIds)
        {
            var tagGroupTags = new List<TagGroupTag>();
            foreach (var tagId in tagIds)
            {
                try
                {
                    tagGroupTags.Add(await _tagGroupTagService.CreateTagGroupTag(tagGroupId, tagId));
                }
                catch
                {
                    Console.WriteLine($"Failed to create tag group tag tag group ID: {tagGroupId}; tag ID: {tagId}."); // TODO: log
                }
            }
            return tagGroupTags;
        }
        #endregion
    }
}
