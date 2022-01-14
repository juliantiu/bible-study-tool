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
        private readonly ITagService _tagService;
        private readonly ITagGroupService _tagGroupService;
        private readonly UserManager<BibleReader> _userManager;

        public Create(INoteReferenceService noteReferenceService,
                      INoteService noteService,
                      ITagService tagService,
                      TagGroupService tagGroupService,
                      UserManager<BibleReader> userManager)
        {
            _noteReferenceService = noteReferenceService;
            _noteService = noteService;
            _tagService = tagService;
            _tagGroupService = tagGroupService;
            _userManager = userManager;
        }
    }
}
