using System;
using System.Collections.Generic;
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
        private readonly IAsyncRepository<NoteReference> _noteReferenceRepository;

        public Create(IAsyncRepository<NoteReference> noteReferenceRepository)
        {
            _noteReferenceRepository = noteReferenceRepository;
        }

        [HttpPost("new")]
        public async Task<ActionResult<CreateNoteReferenceResponse>> CreateHandler(CreateNoteReferenceRequest request)
        {
            try
            {
                return Ok(await CreateHandler(request.NoteReferences, _noteReferenceRepository));
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

        public static async Task<CreateNoteReferenceResponse> CreateHandler(IList<CreateNoteReferenceRequestObject> noteReferenceList,
                                                                            IAsyncRepository<NoteReference> noteReferenceRepository)
        {
            var response = new CreateNoteReferenceResponse();
            var reqNoteReferences = noteReferenceList;
            var noteReferencesCount = reqNoteReferences.Count;
            NoteReference[] noteReferences = new NoteReference[noteReferencesCount];
            for (int i = 0; i < noteReferencesCount; ++i)
            {
                var noteReference = reqNoteReferences[i];
                noteReferences[i] = new NoteReference(noteReference.OwningNoteId, noteReference.ReferencedNoteId, noteReference.ReferencedBibleVerseId);
            }
            await noteReferenceRepository.BulkCreateAsync<NoteReferenceCrudActionException>(noteReferences);
            response.Success = true;
            return response;
        }
    }
}
