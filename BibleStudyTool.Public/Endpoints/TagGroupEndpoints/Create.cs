using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.JoinEntities;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Identity;
using BibleStudyTool.Public.Endpoints.TagGroupTagEndpoints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    [Route("api/tag-group")]
    [ApiController]
    public class Create : ControllerBase
    {
        private readonly IAsyncRepository<TagGroup> _tagGroupRepository;
        private readonly IAsyncRepository<TagGroupTag> _tagGroupTagRepository;
        private readonly UserManager<BibleReader> _userManager;

        public Create(IAsyncRepository<TagGroup> tagGroupRepository,
                      IAsyncRepository<TagGroupTag> tagGroupTagRepository,
                      UserManager<BibleReader> userManager)
        {
            _tagGroupRepository = tagGroupRepository;
            _tagGroupTagRepository = tagGroupTagRepository;
            _userManager = userManager;
        }

        [HttpPost("create")]
        public async Task<ActionResult<CreateTagGroupResponse>> CreateHandler(CreateTagGroupRequest request)
        {
            var tagGroupId = 0;
            var userId = _userManager.GetUserId(User);
            try
            {
                var response = new CreateTagGroupResponse();

                if (request.TagIds.Count < 2)
                {
                    response.FailureMessage = "There needs to be 2 or more tags to constitute a tag grouping.";
                    return Ok(response);
                }

                var tagGroupRef = new TagGroup(userId);
                var tagGroup = await _tagGroupRepository.CreateAsync<TagGroupCrudActionException>(tagGroupRef);
                tagGroupId = tagGroup.TagGroupId;

                var tagGroupTags = new List<CreateTagGroupTagRequestObject>();
                foreach (var tagId in request.TagIds)
                    tagGroupTags.Add(new CreateTagGroupTagRequestObject() { TagGroupId = tagGroupId, TagId = tagId });

                await TagGroupTagEndpoints.Create.CreateHandler(tagGroupTags, _tagGroupTagRepository);

                response.Success = true;
                return Ok(response);
            }
            catch (TagGroupCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (TagGroupTagCrudActionException ex)
            {
                await Delete.DeleteHandler(tagGroupId, userId, _tagGroupRepository, _userManager);
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Message = ex.Message, Timestamp = ex.Timestamp });
            }
            catch (Exception)
            {
                await Delete.DeleteHandler(tagGroupId, userId, _tagGroupRepository, _userManager);
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to create tag group.");
            }
        }
    }
}
