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
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<TagGroupNote> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Create(IAsyncRepository<TagGroupNote> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpPost("api/TagGroupNote")]
        public async Task<ActionResult<CreateTagGroupNoteResponse>> Createhandler(CreateTagGroupNoteRequest request)
        {
            try
            {
                var response = new CreateTagGroupNoteResponse();
                var reqTagGroupNotes = request.TagGroupNotes;
                var tagGroupNotesCount = reqTagGroupNotes.Count;
                TagGroupNote[] tagGroupNotes = new TagGroupNote[tagGroupNotesCount];
                for (int i = 0; i < tagGroupNotesCount; ++i)
                {
                    var tagGroupNote = reqTagGroupNotes[i];
                    tagGroupNotes[i] = new TagGroupNote(tagGroupNote.TagGroupId, tagGroupNote.NoteId);
                }
                await _itemRepository.BulkCreateAsync<TagGroupNoteCrudActionException>(tagGroupNotes);
                response.Success = true;
                return Ok(response);
            }
            catch (TagGroupNoteCrudActionException tncae)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = tncae.Message, Timestamp = tncae.Timestamp });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag note(s).");
            }
        }
    }
}
