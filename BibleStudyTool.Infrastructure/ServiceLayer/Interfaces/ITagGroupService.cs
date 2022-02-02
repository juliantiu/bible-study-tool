using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ITagGroupService
    {
        /// <summary>
        ///     Creates a new tag group.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tagIds"></param>
        /// <returns>
        ///     The newly created tag group along with its associated tags.
        /// </returns>
        Task<TagGroupWithTags> CreateTagGroupAsync
            (string uid, IEnumerable<int> tagIds);

        /// <summary>
        ///     Deletes a tag grouping.
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="tagGroupId"></param>
        /// <returns></returns>
        Task DeleteTagGroupAsync(string uid, int tagGroupId);


        Task<TagGroupWithTags> UpdateTagGroupAsync
            (string uid, int tagGroupId, IEnumerable<int> tagIds);

        /// <summary>
        ///     Gets the user's tag groups.
        /// </summary>
        /// <param name="uid"></param>
        /// <returns>
        ///     The list of tag groups that belong to the user.
        /// </returns>
        Task<IEnumerable<TagGroupWithTags>> GetAllUserTagGroupsAsync(string uid);
    }
}
