using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    [Route("api/tag")]
    [ApiController]
    public class BulkCreate : ControllerBase
    {
        private readonly IAsyncRepository<Tag> _tagRepository;
        private readonly UserManager<BibleReader> _userManager;

        public BulkCreate(IAsyncRepository<Tag> tagRepository,
                      UserManager<BibleReader> usermanager)
        {
            _tagRepository = tagRepository;
            _userManager = usermanager;
        }

        [HttpPost("bulk-create")]
        public async Task<ActionResult<BulkCreateTagsResponse>> BulkCreateHandler(BulkCreateTagsRequest request)
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                return Ok(await BulkCreateHandler(request.Tags, userId, _tagRepository));
            }
            catch (TagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tags.");
            }
        }

        public static async Task<BulkCreateTagsResponse> BulkCreateHandler(IList<TagDto> tagsList, string userId, IAsyncRepository<Tag> tagRepository)
        {
            var response = new BulkCreateTagsResponse();
            await tagRepository.BulkCreateAsync<TagCrudActionException>(tagsList.Select(tag => new Tag(userId, tag.Label, tag.Color)).ToArray());
            response.Success = true;
            return response;
        }
    }
}
