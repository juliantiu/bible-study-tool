using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
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

        [HttpPut("api/Tag")]
        [Authorize]
        public async Task<ActionResult<UpdateTagResponse>> UpdateHandler(UpdateTagRequest request)
        {
            var response = new UpdateTagResponse();
            try
            {
                var currentUserId = _userManager.GetUserId(User);
                var keyId = new Object[] { request.TagId };
                var tag = await _itemRepository.GetByIdAsync<TagCrudActionException>(keyId);
                if (tag.Uid != currentUserId)
                {
                    response.FailureMessage = "The current user does not own the tag being updated.";
                    return response;
                }
                tag.UpdateDetails(request.Label, request.Color);
                await _itemRepository.UpdateAsync<TagCrudActionException>(tag);
                response.Success = true;
                return response;
            }
            catch (TagValidationException ex)
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update tag '{request.Label}.'");
            }
        }
    }
}
