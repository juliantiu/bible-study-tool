using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    [ApiController]
    public class Update : ControllerBase
    {
        private readonly IAsyncRepository<Tag> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Update(IAsyncRepository<Tag> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpPut("api/tag")]
        [Authorize]
        public async Task<ActionResult<UpdateTagResponse>> UpdateHandler(UpdateTagRequest request)
        {
            var response = new UpdateTagResponse();
            try
            {
                var currentUserId = _userManager.GetUserId(User);
                var tag = await _itemRepository.GetByIdAsync<TagCrudActionException>(request.TagId);
                if (tag.Uid != currentUserId)
                {
                    response.FailureMessage = "The current user does not own the tag being updated.";
                    return response;
                }
                tag.UpdateDetails(request.Label);
                await _itemRepository.UpdateAsync<TagCrudActionException>(tag);
                response.Success = true;
                return response;
            }
            catch (TagEmptyLabelException)
            {
                response.Success = false;
                response.FailureMessage = "The new tag label cannot be null.";
                return response;
            }
            catch (TagCrudActionException tcaex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = tcaex.Timestamp, Message = tcaex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update tag '{request.Label}.'");
            }
        }
    }
}
