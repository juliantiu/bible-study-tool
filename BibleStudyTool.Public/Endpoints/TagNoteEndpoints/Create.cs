using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagNoteEndpoints
{
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<TagNote> _itemRepository;

        public Create(IAsyncRepository<TagNote> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("api/TagNote")]
        public async Task<ActionResult<CreateTagNoteResponse>> Createhandler(CreateTagNoteRequest request)
        {
            try
            {
                var response = new CreateTagNoteResponse();
                var reqTagNotes = request.TagNotes;
                var tagNotesCount = reqTagNotes.Count;
                TagNote[] tagNotes = new TagNote[tagNotesCount];
                for (int i = 0; i < tagNotesCount; ++i)
                {
                    var tagNote = reqTagNotes[i];
                    new TagNote(tagNote.TagId, tagNote.NoteId);
                }
                await _itemRepository.BulkCreateAsync<TagNoteCrudActionException>(tagNotes);
                response.Success = true;
                return Ok(response);
            }
            catch (TagNoteCrudActionException tncae)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = tncae.Message, Timestamp = tncae.Timestamp });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag note.");
            }
        }
    }
}
