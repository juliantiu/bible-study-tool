using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupTagEndpoints
{
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<TagGroupTag> _itemRepository;

        public Create(IAsyncRepository<TagGroupTag> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("api/TagGroupTag")]
        public async Task<ActionResult<CreateTagGroupTagResponse>> Createhandler(CreateTagGroupTagRequest request)
        {
            try
            {
                var response = new CreateTagGroupTagResponse();
                var reqTagGroupTags = request.TagGroupTags;
                var tagGroupTagsCount = reqTagGroupTags.Count;
                TagGroupTag[] tagGroupTags = new TagGroupTag[tagGroupTagsCount];
                for (int i = 0; i < tagGroupTagsCount; ++i)
                {
                    var tagGroupTag = reqTagGroupTags[i];
                    tagGroupTags[i] = new TagGroupTag(tagGroupTag.TagGroupId, tagGroupTag.TagId);
                }
                await _itemRepository.BulkCreateAsync<TagGroupTagCrudActionException>(tagGroupTags);
                response.Success = true;
                return Ok(response);
            }
            catch (TagGroupTagCrudActionException tgtcae)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = tgtcae.Message, Timestamp = tgtcae.Timestamp });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag group tag(s).");
            }
        }
    }
}
