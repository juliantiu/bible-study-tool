using System;
using System.Collections.Generic;
using System.Linq;
using BibleStudyTool.Core.Entities;
using BibleStudyTool.Public.DTOs;

namespace BibleStudyTool.Public.Endpoints.NoteTakingEndpoints
{
    public class CreateTagsResponse
    {
        public IEnumerable<TagDto> Tags { get; set; }

        public CreateTagsResponse(IEnumerable<Tag> newtags)
        {
            Tags = newtags.Select(t => new TagDto(t));
        }
    }
}
