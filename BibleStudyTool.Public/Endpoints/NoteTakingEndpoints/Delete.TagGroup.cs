using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public partial class Delete
    {
        [HttpDelete("tag-group")]
        [Authorize]
        public async Task<IActionResult> DeleteTagGroupAsync(int tagGroupId)
        {
            try
            {
                var uid = _userManager.GetUserId(User);
                await _tagGroupService.DeleteTagGroupAsync(uid, tagGroupId);
                return Ok();
            }
            catch (TagGroupCrudActionException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                  new EntityCrudActionExceptionResponse() { Timestamp = ex.Timestamp, Message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete note.");
            }
        }
    }
}
