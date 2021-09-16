using System;
namespace BibleStudyTool.Public.BibleReaderEndpoints
{
    public class RegisterResponse
    {
        public AuthenticateResponse AuthenticateResponse { get; set; }

        public bool Success { get; set; } = false;

        public string Email { get; set; }
        public string FailureMessage { get; set; } = string.Empty;


        public RegisterResponse()
        {
        }
    }
}
