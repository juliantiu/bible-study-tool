using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
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
        private readonly IAsyncRepository<Note> _noteRepository;
        private readonly IAsyncRepository<NoteReference> _noteReferenceRepository;
        private readonly IAsyncRepository<TagNote> _tagNoteRepository;
        private readonly IAsyncRepository<Tag> _tagRepository;
        private readonly UserManager<BibleReader> _userManager;

        public NoteDto CreatedNote = null;

        public Create(IAsyncRepository<Note> noteRepository,
            IAsyncRepository<NoteReference> noteReferenceRepository,
                      IAsyncRepository<TagNote> tagNoteRepository,
                      IAsyncRepository<Tag> tagRepository,
                      UserManager<BibleReader> userManager)
        {
            _noteRepository = noteRepository;
            _noteReferenceRepository = noteReferenceRepository;
            _tagNoteRepository = tagNoteRepository;
            _tagRepository = tagRepository;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateNoteResponse>> CreateHandler(CreateNoteRequest request)
        {
            //string userId =_userManager.GetUserId(User);
            string userId = "1";
            try
            {
                return Ok(await CreateHandler(request.Summary,
                                              request.Text,
                                              request.ExistingTags,
                                              request.NewTags,
                                              request.BibleVerseReferences,
                                              request.NoteReferences,
                                              userId,
                                              _noteRepository,
                                              _noteReferenceRepository,
                                              _tagNoteRepository,
                                              _tagRepository));
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (TagCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (TagNoteCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (NoteReferenceCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create note '{request.Summary}.'");
            }
        }

        public static async Task<CreateNoteResponse> CreateHandler(string summary,
                                                                   string text,
                                                                   IList<int> existingTagIds,
                                                                   IList<TagDto> newTagsList,
                                                                   IList<int> bibleVerseReferences,
                                                                   IList<int> noteReferences,
                                                                   string userId,
                                                                   IAsyncRepository<Note> noteRepository,
                                                                   IAsyncRepository<NoteReference> noteReferenceRepository,
                                                                   IAsyncRepository<TagNote> tagNoteRepository,
                                                                   IAsyncRepository<Tag> tagRepository)
        {
            Note createdNote = null;
            try
            {
                var response = new CreateNoteResponse();

                var noteRef = new Note(userId, summary, text);
                createdNote = await noteRepository.CreateAsync<NoteCrudActionException>(noteRef);

                var newTags = new List<CreateTagResponse>();
                if (newTagsList != null)
                {
                    foreach (var newTag in newTagsList)
                        newTags.Add(await TagEndpoints.Create.CreateHandler(newTag.Label, newTag.Color, userId, tagRepository));
                }

                var tagNoteRequestObjects = new List<CreateTagNoteRequestObject>();
                if (newTags != null)
                {
                    foreach (var newTag in newTags)
                        tagNoteRequestObjects.Add(new CreateTagNoteRequestObject() { TagId = newTag.Tag.TagId, NoteId = createdNote.NoteId });
                }
                if (existingTagIds != null)
                {
                    foreach (var existingTagId in existingTagIds)
                        tagNoteRequestObjects.Add(new CreateTagNoteRequestObject() { TagId = existingTagId, NoteId = createdNote.NoteId });
                }
                if (tagNoteRequestObjects.Count > 0)
                    await TagNoteEndpoints.Create.CreateHandler(tagNoteRequestObjects, tagNoteRepository);

                var noteReferenceRequestObjects = new List<CreateNoteReferenceRequestObject>();
                if (noteReferences != null)
                {
                    foreach (var noteReferenceId in noteReferences)
                    {
                        noteReferenceRequestObjects.Add(new CreateNoteReferenceRequestObject()
                        {
                            OwningNoteId = createdNote.NoteId,
                            ReferencedNoteId = noteReferenceId,
                        });
                    }
                }
                if (bibleVerseReferences != null)
                {
                    foreach (var bibleVerseReferenceId in bibleVerseReferences)
                    {
                        noteReferenceRequestObjects.Add(new CreateNoteReferenceRequestObject()
                        {
                            OwningNoteId = createdNote.NoteId,
                            ReferencedBibleVerseId = bibleVerseReferenceId,
                        });
                    }
                }
                if (noteReferenceRequestObjects.Count > 0)
                    await NoteReferenceEndpoints.Create.CreateHandler(noteReferenceRequestObjects, noteReferenceRepository);

                response.Success = true;
                return response;
            }
            catch (NoteCrudActionException)
            {
                throw;
            }
            catch (TagCrudActionException)
            {
                throw;
            }
            catch (TagNoteCrudActionException)
            {
                throw;
            }
            catch (NoteReferenceCrudActionException)
            {
                throw;
            }
            catch (Exception)
            {
                if (createdNote != null)
                    await Delete.DeleteHandler(createdNote.NoteId, userId, noteRepository);
                throw;
            }
        }
    }
}
