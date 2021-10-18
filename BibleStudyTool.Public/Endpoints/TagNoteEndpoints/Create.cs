using System;
using System.Collections.Generic;
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
        private readonly IAsyncRepository<TagNote> _tagNoteRepository;

        public Create(IAsyncRepository<TagNote> tagNoteRepository)
        {
            _tagNoteRepository = tagNoteRepository;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateTagNoteResponse>> CreateHandler(CreateTagNoteRequest request)
        {
            try
            {
                return Ok(await CreateHandler(request.TagNotes, _tagNoteRepository));
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

        public static async Task<CreateTagNoteResponse> CreateHandler(IList<CreateTagNoteRequestObject> tagNotesList, IAsyncRepository<TagNote> tagNoteRepository)
        {
            var response = new CreateTagNoteResponse();
            if (tagNotesList.Count == 0)
            {
                response.Success = false;
                response.FailureMessage = "Tags list containing the tags to be associated with a note cannot be empty.";
            }
            var reqTagNotes = tagNotesList;
            var tagNotesCount = reqTagNotes.Count;
            TagNote[] tagNotes = new TagNote[tagNotesCount];
            for (int i = 0; i < tagNotesCount; ++i)
            {
                var tagNote = reqTagNotes[i];
                tagNotes[i] = new TagNote(tagNote.TagId, tagNote.NoteId);
            }
            await tagNoteRepository.BulkCreateAsync<TagNoteCrudActionException>(tagNotes);
            response.Success = true;
            return response;
        }
    }
}
