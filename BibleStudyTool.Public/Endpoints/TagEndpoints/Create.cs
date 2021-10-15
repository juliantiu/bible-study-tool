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
    [Route("api/tag")]
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<Tag> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Create(IAsyncRepository<Tag> itemRepository,
                      UserManager<BibleReader> usermanager)
        {
            _itemRepository = itemRepository;
            _userManager = usermanager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateTagResponse>> CreateHandler(CreateTagRequest request)
        {
            try
            {
                var response = new CreateTagResponse();
                var tag = new Tag(_userManager.GetUserId(User), request.Label, request.Color);
                await _itemRepository.CreateAsync<TagCrudActionException>(tag);
                response.Success = true;
                return Ok(response);
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
    }
}
