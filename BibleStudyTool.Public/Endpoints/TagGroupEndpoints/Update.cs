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

        [HttpPut("api/TagGroup")]
        [Authorize]
        public async Task<ActionResult<UpdateTagGroupResponse>> UpdateHandler(UpdateTagGroupRequest request)
        {
            var response = new UpdateTagGroupResponse();
            try
            {
                var currentUserId = _userManager.GetUserId(User);
                var keyId = new Object[] { request.TagGroupId };
                var tagGroup = await _itemRepository.GetByIdAsync<TagGroupCrudActionException>(keyId);
                if (tagGroup.Uid != currentUserId)
                {
                    response.FailureMessage = "The current user does not own the tag group being updated.";
                    return response;
                }
                tagGroup.UpdateDetails(request.Label);
                await _itemRepository.UpdateAsync<TagCrudActionException>(tagGroup);
                response.Success = true;
                return response;
            }
            catch (TagGroupValidationException tgve)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = tgve.Timestamp, Message = tgve.Message });
            }
            catch (TagCrudActionException tcaex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = tcaex.Timestamp, Message = tcaex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update tag group '{request.Label}.'");
            }
        }
    }
}
