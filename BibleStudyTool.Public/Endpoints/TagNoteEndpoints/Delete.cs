using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagNoteEndpoints
{
    [Route("api/tag-note")]
    [ApiController]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<TagNote> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<TagNote> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteTagNoteResponse>> DeleteHandler(DeleteTagNoteRequest request)
        {
            try
            {
                var response = new DeleteTagNoteResponse();
                var reqTagNotes = request.TagNotes;
                var tagNotesCount = reqTagNotes.Count;
                var entityIds = new List<object[]>();
                for (int i = 0; i < tagNotesCount; ++i)
                {
                    var tagNote = reqTagNotes[i];
                    entityIds[i] = new object[] { tagNote.TagId, tagNote.NoteId };
                }
                await _itemRepository.BulkDeleteAsync<TagNoteCrudActionException>(entityIds.ToArray());
                response.Success = true;
                return response;
            }
            catch (TagNoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete tag note associations.");
            }
        }
    }
}
