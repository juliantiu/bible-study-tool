using System;
namespace BibleStudyTool.Public.Endpoints
{
    public class ApiResponseBase
    {
        public bool Success { get; set; } = false;
        public string FailureMessage { get; set; } = string.Empty;

        public ApiResponseBase()
        {
        }
    }
}
