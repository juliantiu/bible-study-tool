using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Infrastructure.ServiceLayer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [Route("api/note")]
    [ApiController]
    public partial class Read : ControllerBase
    {
        private readonly INoteService _noteService;
        private readonly UserManager<BibleReader> _userManager;

        public Read(INoteService noteService,
                   UserManager<BibleReader> userManager)
        {
            _noteService = noteService; 
            _userManager = userManager;
        }
    }
}
