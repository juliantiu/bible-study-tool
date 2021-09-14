using System;
namespace BibleStudyTool.Public.BibleReaderEndpoints
{
    public class AuthenticateResponse
    {
        public bool Result { get; set; } = false;

        public string Email { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string FailureMessage { get; set; } = string.Empty;

        public AuthenticateResponse()
        {
        }
    }
}
