using System;
namespace BibleStudyTool.Public.Endpoints.BibleReaderEndpoints
{
    public class RegisterRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterRequest()
        {
        }
    }
}
