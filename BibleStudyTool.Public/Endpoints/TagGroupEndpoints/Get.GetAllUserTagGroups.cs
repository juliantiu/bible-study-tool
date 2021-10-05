using System;
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
        [HttpGet("api/GetAllUserTagGroupsHandler")]
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
            var tagGroupSpecRef = new TagGroup(uid, string.Empty);
            var tagGroupSpecification = new TagGroupForUserSpecification(tagGroupSpecRef);
            var tagGroupss = await tagGroupRepository.GetBySpecification<TagGroupCrudActionException>(tagGroupSpecification);
            foreach (var tagGroup in tagGroupss)
            {
                response.TagGroups.Add(new TagGroupDto()
                {
                    Label = tagGroup.Label,
                    Uid = tagGroup.Uid,
                    TagGroupId = tagGroup.TagGroupId
                });
            }
            response.Success = true;
            response.Uid = uid;
            return response;
        }
    }
}
