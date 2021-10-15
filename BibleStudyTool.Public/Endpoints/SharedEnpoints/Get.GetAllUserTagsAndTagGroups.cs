using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.SharedEnpoints
{
    public partial class Get
    {
        [HttpGet("get-all-user-tags-and-tag-groups")]
        public async Task<ActionResult<GetAllUserTagsAndTagGroupsResponse>> GetAllUserTagsAndTagGroupsHandler()
        {
            try
            {
                var response = new GetAllUserTagsAndTagGroupsResponse();
                var userId = _userManager.GetUserId(User);
                response.Tags = (await TagEndpoints.Get.GetAllUserTagsHandler(userId, _tagRepository)).Tags;
                response.TagGroups = (await TagGroupEndpoints.Get.GetAllUserTagGroupsHandler(userId, _tagGroupRepository)).TagGroups;
                response.Success = true;
                response.Uid = userId;
                return Ok(response);
            }
            catch (TagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (TagGroupCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get all user tags.");
            }
        }
    }
}
