using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<NoteReference> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Create(IAsyncRepository<NoteReference> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpPost("api/NoteReference")]
        public async Task<ActionResult<CreateNoteReferenceResponse>> CreateHandler(CreateNoteReferenceRequest request)
        {
            try
            {
                var response = new CreateNoteReferenceResponse();
                var reqNoteReferences = request.NoteReferences;
                var noteReferencesCount = reqNoteReferences.Count;
                NoteReference[] noteReferences = new NoteReference[noteReferencesCount];
                for (int i = 0; i < noteReferencesCount; ++i)
                {
                    var noteReference = reqNoteReferences[i];
                    noteReferences[i] = new NoteReference(noteReference.OwningNoteId, noteReference.ReferenceId, noteReference.NoteReferenceType);
                }
                await _itemRepository.BulkCreateAsync<NoteReferenceCrudActionException>(noteReferences);
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
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create note reference(s).");
            }
        }
    }
}
