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
    [ApiController]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<TagGroupTag> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<TagGroupTag> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpDelete("api/TagGroupTags")]
        public async Task<ActionResult<DeleteTagGroupTagResponse>> DeleteHandler(DeleteTagGroupTagRequest request)
        {
            try
            {
                var response = new DeleteTagGroupTagResponse();
                var reqTagGroupTags = request.TagGroupTags;
                var tagGroupNotesCount = reqTagGroupTags.Count;
                var entityIds = new List<object[]>();
                for (int i = 0; i < tagGroupNotesCount; ++i)
                {
                    var tagGroupTag = reqTagGroupTags[i];
                    entityIds[i] = new object[] { tagGroupTag.TagGroupId, tagGroupTag.TagId };
                }
                await _itemRepository.BulkDeleteAsync<TagGroupTagCrudActionException>(entityIds.ToArray());
                response.Success = true;
                return response;
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
    }
}
