using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using BibleStudyTool.Public.DTOs;
using BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints;
using BibleStudyTool.Public.Endpoints.TagEndpoints;
using BibleStudyTool.Public.Endpoints.TagNoteEndpoints;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [Route("api/note")]
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly UserManager<BibleReader> _userManager;

        public NoteDto CreatedNote = null;

        public Create(INoteService noteService, UserManager<BibleReader> userManager)
        {
            _noteService = noteService;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateNoteResponse>> CreateHandler(CreateNoteRequest request)
        {
            string userId =_userManager.GetUserId(User);
            try
            {
                var newTags = (request.NewTags != null) && (request.NewTags.Count > 0)
                            ? request.NewTags.Select(t => new Tag(t.Uid, t.Label, t.Color))
                            : new List<Tag>();
                Note newNote = await _noteService.CreateAsync
                    (userId, request.Summary, request.Text, request.BibleVerseReferences, request.NoteReferences, request.ExistingTags, newTags);

                var newNoteDto = new NoteDto(newNote);
                newNoteDto.References.ReferencedBibleVerses = request.BibleVerseReferences;
                newNoteDto.References.ReferencedNotes = request.NoteReferences;

                var response = new CreateNoteResponse
                {
                    Success = true,
                    Note = newNoteDto
                };
                return Ok(response);
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
