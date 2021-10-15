using System;
using System.Collections.Generic;
using BibleStudyTool.Core.Entities.JoinEntities;

namespace BibleStudyTool.Public.Endpoints.TagGroupEndpoints
{
    public class UpdateTagGroupRequest
    {
        public string TagGroupId { get; set; }
        public IList<TagGroupTag> TagsToDelete { get; set; }
        public IList<TagGroupTag> TagsToAdd { get; set; }

        public UpdateTagGroupRequest()
        {
        }
    }
}
