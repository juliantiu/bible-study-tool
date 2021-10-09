using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagNoteEndpoints
{
    [Route("api/tag-note")]
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<TagNote> _itemRepository;

        public Create(IAsyncRepository<TagNote> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        [HttpPost("create")]
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
            catch (TagNoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag note.");
            }
        }
    }
}
