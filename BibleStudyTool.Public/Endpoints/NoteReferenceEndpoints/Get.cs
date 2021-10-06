using System;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteReferenceEndpoints
{
    [ApiController]
    public partial class Get : ControllerBase
    {
        private readonly IAsyncRepository<NoteReference> _noteReferenceRepository;

        public Get(IAsyncRepository<NoteReference> noteReferenceRepository)
        {
            _noteReferenceRepository = noteReferenceRepository;
        }
    }
}
