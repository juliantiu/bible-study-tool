using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.Interfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleReaderEndpoints
{
    [ApiController]
    public class Register : ControllerBase
    {
        private readonly SignInManager<BibleReader> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;
        private readonly UserManager<BibleReader> _userManager;

        public Register(SignInManager<BibleReader> signInManager,
                        ITokenClaimsService tokenClaimsService,
                        UserManager<BibleReader> userManager)
        {
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
            _userManager = userManager;
        }

        [HttpPost("api/register")]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponse>> RegisterHandler(RegisterRequest request)
        {
            try
            {
                var response = new RegisterResponse();
                var bibleReader = new BibleReader()
                {
                    UserName = request.Email,
                    Email = request.Email
                };
                var result = await _userManager.CreateAsync(bibleReader, request.Password);

                if (result.Succeeded)
                {
                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        var authenticationResponse = new AuthenticateResponse();
                        authenticationResponse.Success = false;
                        authenticationResponse.FailureMessage = "";
                        response.AuthenticateResponse = authenticationResponse;
                    }
                    else
                    {
                        response.Success = true;
                        response.AuthenticateResponse = await TryAuthenticate(request);                      
                    }
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        response.FailureMessage += $"{Environment.NewLine}Error: {error.Code} :: {error.Description}";
                    }
                }

                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to register {request.Email}.");
            }
            throw new NotImplementedException();
        }

        public async Task<AuthenticateResponse> TryAuthenticate(RegisterRequest request)
        {
            var authenticationRequest = new AuthenticateRequest()
            {
                Email = request.Email,
                Password = request.Password
            };
            return await Authenticate.AuthenticateHandler(authenticationRequest, _signInManager, _tokenClaimsService);
        }
    }
}
