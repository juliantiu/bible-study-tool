using System;
namespace BibleStudyTool.Public.BibleReaderEndpoints
{
	public class AuthenticateRequest
	{
		public string Email { get; set; }
		public string Password { get; set; }

		public AuthenticateRequest()
		{			
		}
	}
}
