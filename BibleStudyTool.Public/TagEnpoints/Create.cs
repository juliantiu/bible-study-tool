using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.NonEntityInterfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.TagEnpoints
{
    [ApiController]
    public class Create: ControllerBase
    {
        private readonly IAsyncRepository<Tag> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Create(IAsyncRepository<Tag> itemRepository,
                      UserManager<BibleReader> usermanager)
        {
            _itemRepository = itemRepository;
            _userManager = usermanager;
        }

        [HttpPost("api/Tag")]
        [Authorize]
        public async Task<ActionResult<CreateTagResponse>> CreateHandler(CreateTagRequest request)
        {
            try
            {
                var response = new CreateTagResponse();
                var userId = _userManager.GetUserId(User);
                var tag = new Tag()
                {
                    Label = request.Label,
                    Uid = userId
                };
                var result = await _itemRepository.CreateAsync(tag);
                response.Success = true;
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag '{request.Label}.'");
            }
        }
    }
}
