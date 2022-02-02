using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Exceptions;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public partial class Update
    {
        [HttpPut("tag")]
        [Authorize]
        public async Task<ActionResult<TagDto>> UpdateTag(TagDto request)
        {
            try
            {
                var tag = await _tagService.UpdateTagAsync(request.TagId, request.Uid, request.Label, request.Color);
                return Ok(new TagDto(tag));
            }
            catch (EntityCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (UnauthorizedException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update tag '{request.Label}.'");
            }
        }
    }
}
