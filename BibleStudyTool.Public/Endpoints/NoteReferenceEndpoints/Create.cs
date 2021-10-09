using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    [Route("api/note-reference")]
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

        [HttpPost("new")]
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
            catch (NoteReferenceCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create note reference(s).");
            }
        }
    }
}
