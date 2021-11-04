using System;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    [Route("api/note-taking/update")]
    [ApiController]
    public partial class Update : ControllerBase
    {
        private readonly INoteReferenceService _noteReferenceService;
        private readonly INoteService _noteService;
        private readonly UserManager<BibleReader> _userManager;

        public Update(INoteReferenceService noteReferenceService,
                      INoteService noteService,
                      UserManager<BibleReader> userManager)
        {
            _noteReferenceService = noteReferenceService;
            _noteService = noteService;
            _userManager = userManager;
        }
    }
}
