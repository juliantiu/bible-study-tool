using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
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
        private readonly IAsyncRepository<TagGroup> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<TagGroup> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpDelete("/delete")]
        [Authorize]
        public async Task<ActionResult<DeleteTagGroupResponse>> DeleteHandler(string id)
        {
            try
            {
                var response = new DeleteTagGroupResponse();
                var currentUserId = _userManager.GetUserId(User);
                var idKey = new object[] { id };
                var tagGroup = await _itemRepository.GetByIdAsync<TagGroupCrudActionException>(idKey);
                if (tagGroup.Uid != currentUserId)
                {
                    response.FailureMessage = "The current user does not own the tag group being deleted.";
                    return response;
                }
                await _itemRepository.DeleteAsync<TagGroupCrudActionException>(tagGroup);
                response.Success = true;
                return response;

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
    }
}
