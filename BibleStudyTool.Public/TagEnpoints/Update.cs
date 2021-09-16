using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Core.Entities.Exceptions;
using BibleStudyTool.Core.NonEntityInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.TagEnpoints
{
    public class Update : ControllerBase
    {
        private readonly IAsyncRepository<Tag> _itemRepository;

        public Update(IAsyncRepository<Tag> itemRepository)
        {
            _itemRepository = itemRepository;
        }

        public async Task<ActionResult<UpdateTagResponse>> UpdateHandler(UpdateTagRequest request)
        {
            var response = new UpdateTagResponse();
            try
            {
                var tag = await _itemRepository.GetByIdAsync(request.TagId);
                tag.UpdateDetails(request.Label);
                await _itemRepository.UpdateAsync(tag);
                response.Success = true;
                return response;
            }
            catch (TagEmptyLabelException)
            {
                response.Success = false;
                response.FailureMessage = "The new tag label cannot be null.";
                return response;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to update tag '{request.Label}.'");
            }
        }
    }
}
