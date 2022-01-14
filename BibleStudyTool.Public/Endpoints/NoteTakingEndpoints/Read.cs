using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using BibleStudyTool.Public.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    [Route("api/note-taking/update")]
    [ApiController]
    public class Read : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly ITagGroupService _tagGroupService;
        private readonly ITagService _tagService;
        private readonly UserManager<BibleReader> _userManager;

        public Read(INoteService noteService,
                      ITagGroupService tagGroupService,
                      ITagService tagService,
                      UserManager<BibleReader> userManager)
        {
            _noteService = noteService;
            _tagGroupService = tagGroupService;
            _tagService = tagService;
            _userManager = userManager;
        }

        [HttpGet("get-chapter-notes")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NoteWithTagsAndReferences>>>
            GetChapterNotesAsync(int bibleBookId, int chapterNumber)
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                return Ok(await _noteService.GetAllChapterNotesAsync
                    (uid, bibleBookId, chapterNumber));
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    new EntityCrudActionExceptionResponse()
                        { Message = ex.Message, Timestamp = ex.Timestamp });

            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Failed to get user chapter notes.");
            }
        }

        [HttpGet("get-tags")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TagDto>>> GetUserTagsAsync()
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                return Ok
                    ((await _tagService.GetAllUserTagsAsync(uid))
                                       .Select(t => new TagDto(t)));
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    new EntityCrudActionExceptionResponse()
                        { Message = ex.Message, Timestamp = ex.Timestamp });

            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Failed to get user tags.");
            }
        }

        [HttpGet("get-tag-groups")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<TagGroupWithTags>>>
            GetUserTagGroupsAsync()
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                return Ok
                    (await _tagGroupService.GetAllUserTagGroupsAsync(uid));
            }
            catch (NoteCrudActionException ex)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    new EntityCrudActionExceptionResponse()
                    { Message = ex.Message, Timestamp = ex.Timestamp });

            }
            catch (Exception)
            {
                return StatusCode
                    (StatusCodes.Status500InternalServerError,
                    "Failed to get user tags.");
            }
        }
    }
}
