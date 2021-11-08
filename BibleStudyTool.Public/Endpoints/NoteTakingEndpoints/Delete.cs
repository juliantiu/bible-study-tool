﻿using System;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    [Route("api/note-taking/delete")]
    [ApiController]
    public partial class Delete : ControllerBase
    {
        private readonly INoteReferenceService _noteReferenceService;
        private readonly INoteService _noteService;
        private readonly ITagService _tagService;
        private readonly UserManager<BibleReader> _userManager;

        public Delete(INoteReferenceService noteReferenceService,
                      INoteService noteService,
                      ITagService tagService,
                      UserManager<BibleReader> userManager)
        {
            _noteReferenceService = noteReferenceService;
            _noteService = noteService;
            _tagService = tagService;
            _userManager = userManager;
        }
    }
}
