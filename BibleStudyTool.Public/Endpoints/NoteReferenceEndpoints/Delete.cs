using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    [Route("api/note-reference")]
    [ApiController]
    public class Delete : ControllerBase
    {
        private readonly IAsyncRepository<NoteReference> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(IAsyncRepository<NoteReference> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpDelete("delete")]
        public async Task<ActionResult<DeleteNoteReferenceResponse>> DeleteHandler(DeleteNoteReferenceRequest request)
        {
            try
            {
                var response = new DeleteNoteReferenceResponse();
                var reqNoteReferences = request.NoteReferences;
                var noteReferencesCount = reqNoteReferences.Count;
                var entityIds = new List<object[]>();
                for (int i = 0; i < noteReferencesCount; ++i)
                {
                    var noteReference = reqNoteReferences[i];
                    entityIds[i] = new object[] { noteReference.ReferenceId, noteReference.NoteId };
                }
                await _itemRepository.BulkDeleteAsync<NoteReferenceCrudActionException>(entityIds.ToArray());
                response.Success = true;
                return response;
            }
            catch (NoteReferenceCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete note references.");
            }
        }
    }
}
