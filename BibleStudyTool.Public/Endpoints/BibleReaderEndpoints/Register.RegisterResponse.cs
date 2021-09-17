using System;
namespace BibleStudyTool.Public.Endpoints.BibleReaderEndpoints
{
    public class RegisterResponse : ApiResponseBase
    {
        public AuthenticateResponse AuthenticateResponse { get; set; }
        public string Email { get; set; }

        public RegisterResponse()
        {
        }
    }
}
