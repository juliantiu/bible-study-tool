﻿using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Entities.JoinEntities.Specifications;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    public partial class Get
    {
        [HttpPost("get-all-chapter-notes")]
        public async Task<ActionResult<GetChapterNotesResponse>> GetNotesThatReferencesSpecifiedNotesHandler(int[] noteIds)
        {
            try
            {
                return Ok(await GetNotesThatReferencesSpecifiedNotesHandler(noteIds, _noteReferenceRepository));
            }
            catch (NoteReferenceCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to get note reference(s) for the given bible book chapter.");
            }
        }

        public static async Task<GetChapterNotesResponse> GetNotesThatReferencesSpecifiedNotesHandler(int[] noteIds, IAsyncRepository<NoteReference> noteReferenceRepository)
        {
            var response = new GetChapterNotesResponse();
            var noteReferenceSpecification = new NoteReferenceForOtherNotesSpecification(noteIds);
            var noteReferences = await noteReferenceRepository.GetBySpecification<NoteReferenceCrudActionException>(noteReferenceSpecification);
            foreach (var noteReference in noteReferences)
            {
                response.NoteReferencesForChapter.Add(new NoteReferenceDto()
                {
                    NoteId = noteReference.NoteId,
                    ReferencedNoteId = noteReference.ReferencedNoteId,
                    ReferencedBibleVerseId = noteReference.ReferencedBibleVerseId
                });
            }
            response.Success = true;
            return response;
        }
    }
}
