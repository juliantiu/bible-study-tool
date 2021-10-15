using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupTagEndpoints
{
    [Route("api/tag-group-tag")]
    [ApiController]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<TagGroupTag> _tagGroupTagRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<TagGroupTag> tagGoupTagRepository,
                      UserManager<BibleReader> userManager)
        {
            _tagGroupTagRepository = tagGoupTagRepository;
            _userManager = userManager;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteTagGroupTagResponse>> DeleteHandler(DeleteTagGroupTagRequest request)
        {
            try
            {
                return Ok(await DeleteHandler(request.TagGroupTags, _tagGroupTagRepository));
            }
            catch (TagGroupTagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete tag group tag associations.");
            }
        }

        public static async Task<DeleteTagGroupTagResponse> DeleteHandler(IList<DeleteTagGroupTagRequestObject> tagGroupTags,
                                                                          IAsyncRepository<TagGroupTag> tagGroupTagRepository)
        {
            var response = new DeleteTagGroupTagResponse();
            var reqTagGroupTags = tagGroupTags;
            var tagGroupNotesCount = reqTagGroupTags.Count;
            var entityIds = new List<object[]>();
            for (int i = 0; i < tagGroupNotesCount; ++i)
            {
                var tagGroupTag = reqTagGroupTags[i];
                entityIds[i] = new object[] { tagGroupTag.TagGroupId, tagGroupTag.TagId };
            }
            await tagGroupTagRepository.BulkDeleteAsync<TagGroupTagCrudActionException>(entityIds.ToArray());
            response.Success = true;
            return response;
        }
    }
}
