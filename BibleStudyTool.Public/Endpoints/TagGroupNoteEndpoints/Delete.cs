using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupNoteEndpoints
{
    [ApiController]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<TagGroupNote> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<TagGroupNote> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpDelete("api/TagGroupNotes")]
        public async Task<ActionResult<DeleteTagGroupNoteResponse>> DeleteHandler(DeleteTagGroupNoteRequest request)
        {
            try
            {
                var response = new DeleteTagGroupNoteResponse();
                var reqTagGroupNotes = request.TagGroupNotes;
                var tagGroupNotesCount = reqTagGroupNotes.Count;
                var entityIds = new List<object[]>();
                for (int i = 0; i < tagGroupNotesCount; ++i)
                {
                    var tagGroupNote = reqTagGroupNotes[i];
                    entityIds[i] = new object[] { tagGroupNote.TagGroupId, tagGroupNote.NoteId };
                }
                await _itemRepository.BulkDeleteAsync<TagGroupNoteCrudActionException>(entityIds.ToArray());
                response.Success = true;
                return response;
            }
            catch (TagGroupNoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete tag group note associations.");
            }
        }
    }
}
