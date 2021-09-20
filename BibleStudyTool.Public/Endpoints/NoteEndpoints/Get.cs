using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteEndpoints
{
    [ApiController]
    public partial class Get : ControllerBase
    {
        private readonly IAsyncRepository<Note> _itemRepository;
        private readonly IEntityGetterRepoFactory<NoteGetterRepository> _entityGetterRepoFactory;
        private readonly INoteGetterRepository _noteGetterRepository;

        public Get(IAsyncRepository<Note> itemRepository,
                          IEntityGetterRepoFactory<NoteGetterRepository> entityGetterRepoFactory)
        {
            _itemRepository = itemRepository;
            _entityGetterRepoFactory = entityGetterRepoFactory;

            _noteGetterRepository = (INoteGetterRepository)_entityGetterRepoFactory.CreateRepository();
        }
    }
}
