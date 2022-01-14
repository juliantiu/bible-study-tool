using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;

namespace BibleStudyTool.Infrastructure.ServiceLayer
{
    public interface ITagService
    {
        /// <summary>
        ///     Bulk creates tags.
        /// </summary>
        /// <param name="newTags"></param>
        /// <returns>
        ///     The list of created tags.
        /// </returns>
        Task<IEnumerable<Tag>> CreateTagsAsync(IEnumerable<Tag> newTags);

        /// <summary>
        ///     Creates a tag.
        /// </summary>
        /// <param name="newTags"></param>
        /// <returns>
        ///     The created tag.
        /// </returns>
        Task<Tag> CreateTagAsync(string uid, string label, string color);

        /// <summary>
        ///     Updates the tag.
        /// </summary>
        /// <param name="tagId"></param>
        /// <param name="uid"></param>
        /// <param name="label"></param>
        /// <param name="color"></param>
        /// <returns>
        ///     The updated tag.
        /// </returns>
        Task<Tag> UpdateTagAsync
            (int tagId, string uid, string label, string color);

        /// <summary>
        ///     Deletes the tag
        /// </summary>
        /// <param name="tagId"></param>
        /// <returns></returns>
        Task DeleteTagAsync(string uid, int tagId);

        /// <summary>
        ///     Gets all user tags.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>
        ///     The list of tags that belong to the user.
        /// </returns>
        Task<IEnumerable<Tag>> GetAllUserTagsAsync(string userId);
    }
}
