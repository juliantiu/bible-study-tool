using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public partial class Create
    {
        [HttpPost("note")]
        [Authorize]
        public async Task<ActionResult<CreateNoteResponse>> CreateNoteAsync(CreateNoteRequest request)
        {
            string userId = _userManager.GetUserId(User);
            try
            {
                var newTags = (request.NewTags != null) && (request.NewTags.Count() > 0)
                            ? request.NewTags.Select(t => new Tag(t.Uid, t.Label, t.Color))
                            : new List<Tag>();
                var newNote = await _noteService.CreateAsync
                    (userId, request.Summary, request.Text, request.BibleVerseReferences, request.NoteReferences, request.ExistingTags, newTags);

                return Ok(new CreateNoteResponse(newNote));
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create note '{request.Summary}.'");
            }
        }
    }
}
