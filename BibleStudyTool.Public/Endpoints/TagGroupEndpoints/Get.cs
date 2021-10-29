using System;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    [Route("api/tag-group")]
    [ApiController]
    public partial class Get : ControllerBase
    {
        private readonly IAsyncRepository<TagGroup> _tagGroupRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Get(IAsyncRepository<TagGroup> tagGroupRepository,
                   UserManager<BibleReader> userManager)
        {
            _tagGroupRepository = tagGroupRepository;
            _userManager = userManager;
        }
    }
}
