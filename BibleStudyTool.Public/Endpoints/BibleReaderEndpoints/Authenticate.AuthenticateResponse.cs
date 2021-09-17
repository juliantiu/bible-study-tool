using System;
namespace BibleStudyTool.Public.Endpoints.BibleReaderEndpoints
{
    public class AuthenticateResponse : ApiResponseBase
    {
        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;

        public AuthenticateResponse()
        {
        }
    }
}
