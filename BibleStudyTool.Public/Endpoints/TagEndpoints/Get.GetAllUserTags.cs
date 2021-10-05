using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Specifications;
using BibleStudyTool.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagEndpoints
{
    public partial class Get
    {
        [Authorize]
        [HttpGet("api/GetAllUserTagsHandler")]
        public async Task<ActionResult<GetAllUserTagsResponse>> GetAllUserTagsHandler()
        {
            try
            {
                var userId = _userManager.GetUserId(User);
                return Ok(await GetAllUserTagsHandler(userId, _tagRepository));
            }
            catch (TagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to get all user tags.");
            }
        }

        public static async Task<GetAllUserTagsResponse> GetAllUserTagsHandler(string uid,
                                                                               IAsyncRepository<Tag> tagRepository)
        {
            var response = new GetAllUserTagsResponse();
            var tagSpecRef = new Tag(uid, string.Empty, string.Empty);
            var tagSpecification = new TagForUserSpecification(tagSpecRef);
            var tags = await tagRepository.GetBySpecification<TagCrudActionException>(tagSpecification);
            foreach (var tag in tags)
            {
                response.Tags.Add(new DTOs.TagDto()
                {
                    Color = tag.Color,
                    Label = tag.Label,
                    TagId = tag.TagId,
                    Uid = tag.Uid
                });
            }
            response.Success = true;
            response.Uid = uid;
            return response;
        }
    }
}
