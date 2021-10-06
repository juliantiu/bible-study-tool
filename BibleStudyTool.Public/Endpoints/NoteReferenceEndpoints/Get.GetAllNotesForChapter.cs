using System;
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
        [HttpPost("api/GetAllNotesForChapter")]
        public async Task<ActionResult<GetAllNotesForChapterResponse>> GetAllNotesForChapterHandler(int[] bibleVerseIds)
        {
            try
            {
                return Ok(await GetAllNotesForChapterHandler(bibleVerseIds, _noteReferenceRepository));
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

        public static async Task<GetAllNotesForChapterResponse> GetAllNotesForChapterHandler(int[] bibleVerseIds, IAsyncRepository<NoteReference> noteReferenceRepository)
        {
            var response = new GetAllNotesForChapterResponse();
            var noteReferenceSpecification = new NoteReferenceForChapterSpecification(bibleVerseIds);
            var noteReferences = await noteReferenceRepository.GetBySpecification<NoteReferenceCrudActionException>(noteReferenceSpecification);
            foreach (var noteReference in noteReferences)
            {
                response.NoteReferencesForChapter.Add(new NoteReferenceDto()
                {
                    NoteId = noteReference.NoteId,
                    NoteReferenceType = noteReference.NoteReferenceType,
                    ReferenceId = noteReference.ReferenceId
                });
            }
            response.Success = true;
            return response;
        }
    }
}
