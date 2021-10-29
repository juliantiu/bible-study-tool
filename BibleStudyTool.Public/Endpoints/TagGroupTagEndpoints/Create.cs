using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupTagEndpoints
{
    [Route("api/tag-group-tag")]
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<TagGroupTag> _tagGroupTagRepository;

        public Create(IAsyncRepository<TagGroupTag> tagGroupTagRepository)
        {
            _tagGroupTagRepository = tagGroupTagRepository;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateTagGroupTagResponse>> CreateHandler(CreateTagGroupTagRequest request)
        {
            try
            {
                return Ok(await CreateHandler(request.TagGroupTags, _tagGroupTagRepository));
            }
            catch (TagGroupTagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag group tag(s).");
            }
        }

        public static async Task<CreateTagGroupTagResponse> CreateHandler(IList<CreateTagGroupTagRequestObject> tagGroupTagsList,
                                                                          IAsyncRepository<TagGroupTag> tagGroupTagRepository)
        {
            var response = new CreateTagGroupTagResponse();
            var reqTagGroupTags = tagGroupTagsList;
            var tagGroupTagsCount = reqTagGroupTags.Count;
            TagGroupTag[] tagGroupTags = new TagGroupTag[tagGroupTagsCount];
            for (int i = 0; i < tagGroupTagsCount; ++i)
            {
                var tagGroupTag = reqTagGroupTags[i];
                tagGroupTags[i] = new TagGroupTag(tagGroupTag.TagGroupId, tagGroupTag.TagId);
            }
            await tagGroupTagRepository.BulkCreateAsync<TagGroupTagCrudActionException>(tagGroupTags);
            response.Success = true;
            return response;
        }
    }
}
