using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.NonEntityInterfaces;
using BibleStudyTool.Infrastructure.Data;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<Note> _itemRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Create(IAsyncRepository<Note> itemRepository,
                      UserManager<BibleReader> userManager)
        {
            _itemRepository = itemRepository;
            _userManager = userManager;
        }

        [HttpPost("api/note")]
        [Authorize]
        public async Task<ActionResult<CreateNoteResponse>> CreateHandler(CreateNoteRequest request)
        {
            try
            {
                var response = new CreateNoteResponse();
                var note = new Note()
                {
                    Summary = request.Summary,
                    Text = request.Text,
                    Uid = _userManager.GetUserId(User)
                };
                await _itemRepository.CreateAsync(note);
                response.Success = true;
                return Ok(response);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create note '{request.Summary}.'");
            }
        }
    }
}
