using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using PollBack.Core.Interfaces.Services;
using PollBack.Core.Models;
using PollBack.Web.ViewModels;

namespace PollBack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;
        private readonly IMapper mapper;

        public AuthenticationController(IAuthenticationService authenticationService, IMapper mapper)
        {
            this.authenticationService = authenticationService;
            this.mapper = mapper;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignUp(SignUpRequest signUpRequest)
        {
            try
            {
                await authenticationService.SignUpAsync(signUpRequest);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SignIn(SignInRequest signInRequest)
        {
            try
            {
                signInRequest.IpAddress = GetIpAddress();

                SignInResponse signInResponse = await authenticationService.SignInAsync(signInRequest);

                SetTokenCookie(signInResponse.RefreshToken);

                SignInResponseViewModel signInResponseViewModel = mapper.Map<SignInResponseViewModel>(signInResponse);

                return Ok(signInResponseViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                string? refreshToken = Request.Cookies["refreshToken"];

                if (refreshToken == null)
                    return BadRequest("Refresh token not found.");

                SignInResponse signInResponse = await authenticationService.RefreshTokenAsync(refreshToken, GetIpAddress());

                SetTokenCookie(signInResponse.RefreshToken);

                SignInResponseViewModel signInResponseViewModel = mapper.Map<SignInResponseViewModel>(signInResponse);

                return Ok(signInResponseViewModel);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RevokeToken(RevokeToken revokeToken)
        {
            try
            {
                string? token = revokeToken.Token ?? Request.Cookies["refreshToken"];

                if (string.IsNullOrEmpty(token))
                    return BadRequest("Refresh token not found.");

                await authenticationService.RevokeTokenAsync(token, GetIpAddress());

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
            {
                string? ip = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

                return ip ?? string.Empty;
            }            
        }

        private void SetTokenCookie(string token)
        {
            CookieOptions cookieOptions = new()
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }
    }
}
