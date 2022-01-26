using System;
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
        private readonly IGroupedTagService _tagGroupTagService;
        private readonly TagGroupQueries _tagGroupQueries;
        private readonly TagQueries _tagQueries;

        public TagGroupService(IAsyncRepository<TagGroup> tagGroupRepository,
                               IGroupedTagService tagGroupTagService,
                               TagGroupQueries tagGroupQueries,
                               TagQueries tagQueries)
        {
            _tagGroupRepository = tagGroupRepository;
            _tagGroupTagService = tagGroupTagService;
            _tagGroupQueries = tagGroupQueries;
            _tagQueries = tagQueries;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public async Task<TagGroupWithTags> CreateTagGroupAsync
            (string uid, IEnumerable<int> tagIds)
        {
            // Create the new tag group
            var tagGroup = new TagGroup(uid);

            var newTagGroup =
                await _tagGroupRepository
                    .CreateAsync<EntityCrudActionException>
                        (tagGroup);

            var tagGroupId = newTagGroup.Id;

            // Associate the input tag IDs with the newly created tag group
            var groupedTags = await associateTagsWithTagGroup
                (tagGroupId, tagIds);

            newTagGroup.AssignTags(groupedTags);

            // Get tags with the tag IDs associated to the tag group
            var tags = await _tagQueries.GetTagsInTagGroupAsync(tagGroupId);
                
            return new TagGroupWithTags(newTagGroup, tags);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tagGroupId"></param>
        /// <returns></returns>
        public async Task DeleteTagGroupAsync(string uid, int tagGroupId)
        {
            TagGroup tagGroup =
                await _tagGroupRepository.GetByIdAsync<TagCrudActionException>
                    (new object[1] { tagGroupId });
            if (tagGroup == null)
            {
                throw new Exception
                    ("The tag group to be deleted does not exist.");
            }
            else if (tagGroup.Uid != uid)
            {
                throw new UnauthorizedException
                ("The logged in user does not own the" +
                " tag group to be deleted.");
            }
            await _tagGroupRepository.DeleteAsync<TagCrudActionException>
                (tagGroup);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tagGroupId"></param>
        /// <param name="tagIds"></param>
        /// <returns></returns>
        public async Task<TagGroupWithTags> UpdateTagGroupAsync
            (string uid, int tagGroupId, IEnumerable<int> tagIds)
        {
            TagGroup tagGroup =
                await _tagGroupRepository
                    .GetByIdAsync<TagCrudActionException>
                        (new object[1] { tagGroupId });
            if (tagGroup == null)
            {
                throw new Exception
                    ("The tag group to be updated does not exist.");
            }
            else if (tagGroup.Uid != uid)
            {
                throw new UnauthorizedException
                    ("The logged in user does not own the" +
                    " tag group to be updated.");
            }

            // Get all tags associated with the tag group
            var tagsInTagGroup = await _tagQueries.GetTagsInTagGroupAsync
                (tagGroupId);

            // Determine which tags need to be deleted
            var tagsToBeDeleted = determineTagsToBeDeleted
                (tagsInTagGroup, tagIds);

            // Delete tags that need to be deleted
            await removeTagsFromTagGroupAsync(tagGroupId, tagsToBeDeleted);

            // Determine which tags need to be added
            var tagsToBeAdded = determineTagsToBeAdded(tagsInTagGroup, tagIds);

            // Add tags that need to be added
            var tagGroupTags = await associateTagsWithTagGroup
                (tagGroupId, tagsToBeAdded);
            tagGroup.AssignTags(tagGroupTags);

            // Get tags with the tag IDs associated to the tag group after
            // the deletion and additions
            var tags = await _tagQueries.GetTagsInTagGroupAsync(tagGroupId);

            return new TagGroupWithTags(tagGroup, tags);
        }

        /// <summary>
        ///     Gets the user's tag groups.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>
        ///     The list of tag groups that belong to the user.
        /// </returns>
        public Task<IEnumerable<TagGroupWithTags>> GetAllUserTagGroupsAsync
            (string uid)
        {
            return _tagGroupQueries.GetAllUserTagGroupsAsync(uid);
        }

        #region************************************************** HELPER METHODS
        // *********************************************************************
        // *********************************************************************

        private async Task<IEnumerable<GroupedTag>> associateTagsWithTagGroup
            (int tagGroupId, IEnumerable<int> tagIds)
        {
            var groupedTags = new List<GroupedTag>();
            foreach (var tagId in tagIds)
            {
                try
                {
                    groupedTags.Add
                        (await _tagGroupTagService.CreateGroupedTags
                            (tagGroupId, tagId));
                }
                catch
                {
                    Console.WriteLine
                        ($"Failed to create tag group tag tag group ID:" +
                        $" {tagGroupId}; tag ID: {tagId}."); // TODO: log
                }
            }
            return groupedTags;
        }

        private IEnumerable<int> determineTagsToBeDeleted
            (IEnumerable<Tag> tags, IEnumerable<int> tagIds)
        {
            IList<int> tagsToBeDeleted = new List<int>();
            foreach (var tag in tags)
            {
                var tagGroupTag_tagId = tag.Id;
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
                if (tags.Any(tgt => tgt.Id == tagId) == false)
                {
                    tagsToBeAdded.Add(tagId);
                }
            }
            return tagsToBeAdded;
        }

        private async Task removeTagsFromTagGroupAsync
            (int tagGroupId, IEnumerable<int> tagIds)
        {
            try
            {
                await _tagGroupTagService.RemoveTagsFromTagGroup
                    (tagGroupId, tagIds);
            }
            catch
            {
                Console.WriteLine
                    ("Failed to remove all or some tags from the tag group."); //TODO: log
            }
        }
        #endregion
    }
}
