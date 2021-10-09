using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [Route("api/note")]
    [ApiController]
    public partial class Get : ControllerBase
    {
        private readonly IAsyncRepository<Note> _noteRepository;
        private readonly IEntityGetterRepoFactory<NoteGetterRepository> _entityGetterRepoFactory;

        private readonly UserManager<BibleReader> _userManager;

        private readonly INoteGetterRepository _noteGetterRepository;

        public Get(IAsyncRepository<Note> noteRepository,
                   IEntityGetterRepoFactory<NoteGetterRepository> entityGetterRepoFactory,
                   UserManager<BibleReader> userManager)
        {
            _noteRepository = noteRepository;
            _entityGetterRepoFactory = entityGetterRepoFactory;

            _userManager = userManager;

            _noteGetterRepository = (INoteGetterRepository)_entityGetterRepoFactory.CreateRepository();
        }
    }
}
