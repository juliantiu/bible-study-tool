using System;
namespace BibleStudyTool.Public.BibleReaderEndpoints
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
