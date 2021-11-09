﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Exceptions;
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

        public async Task DeleteTagGroupAsync(string uid, int tagGroupId)
        {
            TagGroup tagGroup = await _tagGroupRepository.GetByIdAsync<TagCrudActionException>(new object[1] { tagGroupId });
            if (tagGroup == null)
            {
                throw new Exception("The tag group to be deleted does not exist.");
            }
            else if (tagGroup.Uid != uid)
            {
                throw new UnauthorizedException("The logged in user does not own the tag group to be deleted.");
            }
            await _tagGroupRepository.DeleteAsync<TagCrudActionException>(tagGroup);
        }

        public async Task<TagGroupWithTags> UpdateTagGroupAsync(string uid, int tagGroupId, IEnumerable<int> tagIds)
        {
            TagGroup tagGroup = await _tagGroupRepository.GetByIdAsync<TagCrudActionException>(new object[1] { tagGroupId });
            if (tagGroup == null)
            {
                throw new Exception("The tag group to be updated does not exist.");
            }
            else if (tagGroup.Uid != uid)
            {
                throw new UnauthorizedException("The logged in user does not own the tag group to be updated.");
            }

            // Get all tags associated with the tag group
            var tagsInTagGroup = await _tagQueries.GetTagsInTagGroupAsync(tagGroupId);

            // Determine which tags need to be deleted
            var tagsToBeDeleted = determineTagsToBeDeleted(tagsInTagGroup, tagIds);

            // Delete tags that need to be deleted
            await removeTagsFromTagGroupAsync(tagGroupId, tagsToBeDeleted);

            // Determine which tags need to be added
            var tagsToBeAdded = determineTagsToBeAdded(tagsInTagGroup, tagIds);

            // Add tags that need to be added
            var tagGroupTags = await associateTagsWithTagGroup(tagGroupId, tagsToBeAdded);
            tagGroup.AssignTags(tagGroupTags);

            // Get tags with the tag IDs associated to the tag group after the deletion and additions
            var tags = await _tagQueries.GetTagsInTagGroupAsync(tagGroupId);

            return new TagGroupWithTags(tagGroup, tags);
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

        private IEnumerable<int> determineTagsToBeDeleted
            (IEnumerable<Tag> tags, IEnumerable<int> tagIds)
        {
            IList<int> tagsToBeDeleted = new List<int>();
            foreach (var tag in tags)
            {
                var tagGroupTag_tagId = tag.TagId;
                if (tagIds.Contains(tagGroupTag_tagId) == false)
                {
                    tagsToBeDeleted.Add(tagGroupTag_tagId);
                }
            }
            return tagsToBeDeleted;
        }

        private IEnumerable<int> determineTagsToBeAdded
            (IEnumerable<Tag> tags, IEnumerable<int> tagIds)
        {
            IList<int> tagsToBeAdded = new List<int>();
            foreach (var tagId in tagIds)
            {
                if (tags.Any(tgt => tgt.TagId == tagId) == false)
                {
                    tagsToBeAdded.Add(tagId);
                }
            }
            return tagsToBeAdded;
        }

        private async Task removeTagsFromTagGroupAsync(int tagGroupId, IEnumerable<int> tagIds)
        {
            try
            {
                await _tagGroupTagService.RemoveTagsFromTagGroup(tagGroupId, tagIds);
            }
            catch
            {
                Console.WriteLine("Failed to remove all or some tags from the tag group."); //TODO: log
            }
        }
        #endregion
    }
}
