using System;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Specifications;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    public partial class Get
    {
        [Authorize]
        [HttpGet("get-all-user-tag-groups")]
        public async Task<ActionResult<GetAllUserTagGroupsResponse>> GetAllUserTagGroupsHandler()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                return Ok(await GetAllUserTagGroupsHandler(userId, _tagGroupRepository));
            }
            catch (TagGroupCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get all user tag groups.");
            }
        }

        public static async Task<GetAllUserTagGroupsResponse> GetAllUserTagGroupsHandler(string uid,
                                                                                         IAsyncRepository<TagGroup> tagGroupRepository)
        {
            var response = new GetAllUserTagGroupsResponse();
            var tagGroupSpecRef = new TagGroup(uid);
            var tagGroupSpecification = new TagGroupForUserSpecification(tagGroupSpecRef);
            var tagGroupss = await tagGroupRepository.GetBySpecification<TagGroupCrudActionException>(tagGroupSpecification);
            foreach (var tagGroup in tagGroupss)
            {
                response.TagGroups.Add(new TagGroupDto()
                {
                    Uid = tagGroup.Uid,
                    TagGroupId = tagGroup.TagGroupId,
                    Tags = tagGroup.TagGroupTags.Select(tagGroupTag => new TagDto
                    {
                        Color = tagGroupTag.Tag.Color,
                        Label = tagGroupTag.Tag.Label,
                        TagId = tagGroupTag.Tag.TagId,
                        Uid = tagGroupTag.Tag.Uid
                    }).ToList()
                });
            }
            response.Success = true;
            response.Uid = uid;
            return response;
        }
    }
}
