using System;
using System.Threading.Tasks;
using BibleStudyTool.Core.NonEntityInterfaces;
using BibleStudyTool.Infrastructure.Data.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BibleStudyTool.Public.Endpoints.BibleReaderEndpoints
{
    [ApiController]
    public class Authenticate : ControllerBase
    {
        private readonly SignInManager<BibleReader> _signInManager;
        private readonly ITokenClaimsService _tokenClaimsService;

        public Authenticate(SignInManager<BibleReader> signInManager,
                            ITokenClaimsService tokenClaimsService)
        {
            _signInManager = signInManager;
            _tokenClaimsService = tokenClaimsService;
        }

        [HttpPost("api/authenticate")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthenticateResponse>> AuthenticateHandler(AuthenticateRequest request)
        {
            try
            {
                var response = new AuthenticateResponse();
                var result = await _signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

                response.Email = request.Email;
                response.Success = result.Succeeded;

                if (result.Succeeded)
                    response.Token = await _tokenClaimsService.GetTokenAsync(request.Email);

                return Ok(await AuthenticateHandler(request, _signInManager, _tokenClaimsService));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Failed to log in user {request.Email}.");
            }
        }

        public static async Task<AuthenticateResponse> AuthenticateHandler(AuthenticateRequest request,
                                                                           SignInManager<BibleReader> signInManager,
                                                                           ITokenClaimsService tokenClaimsService)
        {
            var response = new AuthenticateResponse();
            var result = await signInManager.PasswordSignInAsync(request.Email, request.Password, false, true);

            response.Email = request.Email;
            response.Success = result.Succeeded;

            if (result.Succeeded)
                response.Token = await tokenClaimsService.GetTokenAsync(request.Email);
            else
                response.FailureMessage = $"Failed to log in user {request.Email}.";

            return response;
        }
    }
}
