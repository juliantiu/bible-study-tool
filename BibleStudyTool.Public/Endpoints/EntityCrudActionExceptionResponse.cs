using System;
namespace BibleStudyTool.Public.Endpoints
{
    public struct EntityCrudActionExceptionResponse
    {
        public DateTimeOffset Timestamp { get; set; }
        public string Message { get; set; }
    }
}
