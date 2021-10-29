using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    [Route("api/tag")]
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<Tag> _tagRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Create(IAsyncRepository<Tag> itemRepository,
                      UserManager<BibleReader> usermanager)
        {
            _tagRepository = itemRepository;
            _userManager = usermanager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateTagResponse>> CreateHandler(CreateTagRequest request)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                return Ok(await CreateHandler(request.Label, request.Color, userId, _tagRepository));
            }
            catch (TagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag '{request.Label}.'");
            }
        }

        public static async Task<CreateTagResponse> CreateHandler(string label, string color, string userId, IAsyncRepository<Tag> tagRepository)
        {
            var response = new CreateTagResponse();
            var tagRef = new Tag(userId, label, color);
            response.Tag = await tagRepository.CreateAsync<TagCrudActionException>(tagRef);
            response.Success = true;
            return response;
        }
    }
}
