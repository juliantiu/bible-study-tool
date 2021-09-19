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
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<Tag> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<Tag> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpDelete("api/tag")]
        [Authorize]
        public async Task<ActionResult<DeleteTagResponse>> DeleteHandler(string id)
        {
            try
            {
                var response = new DeleteTagResponse();
                var currentUserId = _userManager.GetUserId(User);
                var tag = await _itemRepository.GetByIdAsync(id);
                if (tag.Uid != currentUserId)
                {
                    response.FailureMessage = "The current user does not own the tag being deleted.";
                    return response;
                }
                await _itemRepository.DeleteAsync(tag);
                response.Success = true;
                return response;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete tag.");
            }
        }
    }
}
