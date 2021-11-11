using System;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public partial class Update
    {
        [HttpPut("note")]
        [Authorize]
        public async Task<ActionResult<NoteWithTagsAndReferences>> UpdateNote(UpdateNoteRequest request)
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                var newTags = request.NewTags.Select(t => new Tag(uid, t.Label, t.Color));
                var updatedNote = await _noteService.UpdateAsync
                    (request.NoteId, uid, request.Summary, request.Text,
                    request.TagIds, request.BibleReferenceIds, request.NoteReferenceIds,
                    newTags);
                return updatedNote;
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (UnauthorizedException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update note '{request.Summary}.'");
            }
        }
    }
}
