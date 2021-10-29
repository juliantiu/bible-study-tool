using System;
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
    public class Update : ControllerBase
    {
        private readonly IAsyncRepository<TagGroup> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Update(IAsyncRepository<TagGroup> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpPut("/update")]
        [Authorize]
        public ActionResult<UpdateTagGroupResponse> UpdateHandler(UpdateTagGroupRequest request)
        {
            var response = new UpdateTagGroupResponse();
            try
            {
                //var currentUserId = _userManager.GetUserId(User);
                //var keyId = new Object[] { request.TagGroupId };
                //var tagGroup = await _itemRepository.GetByIdAsync<TagGroupCrudActionException>(keyId);
                //if (tagGroup.Uid != currentUserId)
                //{
                //    response.FailureMessage = "The current user does not own the tag group being updated.";
                //    return response;
                //}
                //tagGroup.UpdateDetails(request.Label);
                //await _itemRepository.UpdateAsync<TagCrudActionException>(tagGroup);
                //response.Success = true;
                return response;
            }
            catch (TagGroupValidationException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (TagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update tag group.");
            }
        }
    }
}
