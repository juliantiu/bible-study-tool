using System;
namespace BibleStudyTool.Public.Endpoints
{
    public struct EntityCrudActionExceptionResponse
    {
        public DateTime Timestamp { get; set; }
        public string Message { get; set; }
    }
}
