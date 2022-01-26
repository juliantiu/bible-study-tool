using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public partial class Create
    {
        [HttpPost("tags")]
        [Authorize]
        public async Task<ActionResult<CreateTagsResponse>> CreateTagsAsync([FromBody]IEnumerable<TagDto> request)
        {
            string userId = _userManager.GetUserId(User);
            try
            {
                var tags = request.Select(t => new Tag(t.Uid, t.Color, t.Label));
                var newTags = await _tagService.CreateTagsAsync(tags);
                return Ok(new CreateTagsResponse(newTags));
            }
            catch (EntityCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create some or all tags.");
            }
        }
    }
}
