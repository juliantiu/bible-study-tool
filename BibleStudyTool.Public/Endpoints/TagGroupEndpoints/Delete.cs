using System;
using System.Security.Claims;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    [Route("api/tag-group")]
    [ApiController]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<TagGroup> _tagGroupRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<TagGroup> tagGroupRepository,
                      UserManager<BibleReader> userManager)
        {
            _tagGroupRepository = tagGroupRepository;
            _userManager = userManager;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteTagGroupResponse>> DeleteHandler(int id)
        {
            try
            {
                return Ok(await DeleteHandler(id, _userManager.GetUserId(User), _tagGroupRepository, _userManager));

            }
            catch (TagGroupCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete tag group.");
            }
        }

        public static async Task<DeleteTagGroupResponse> DeleteHandler(int id,
                                                                       string uid,
                                                                       IAsyncRepository<TagGroup> tagGroupRepository,
                                                                       UserManager<BibleReader> userManager)
        {
            var response = new DeleteTagGroupResponse();
            var currentUserId = uid;
            var idKey = new object[] { id };
            var tagGroup = await tagGroupRepository.GetByIdAsync<TagGroupCrudActionException>(idKey);
            if (tagGroup.Uid != currentUserId)
            {
                response.FailureMessage = "The current user does not own the tag group being deleted.";
                return response;
            }
            await tagGroupRepository.DeleteAsync<TagGroupCrudActionException>(tagGroup);
            response.Success = true;
            return response;
        }
    }
}
