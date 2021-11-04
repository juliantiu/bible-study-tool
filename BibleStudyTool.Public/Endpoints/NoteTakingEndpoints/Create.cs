using System;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    [Route("api/note-taking/create")]
    [ApiController]
    public partial class Create : ControllerBase
    {
        private readonly INoteReferenceService _noteReferenceService;
        private readonly INoteService _noteService;
        private readonly UserManager<BibleReader> _userManager;

        public Create(INoteReferenceService noteReferenceService,
                      INoteService noteService,
                      UserManager<BibleReader> userManager)
        {
            _noteReferenceService = noteReferenceService;
            _noteService = noteService;
            _userManager = userManager;
        }
    }
}
