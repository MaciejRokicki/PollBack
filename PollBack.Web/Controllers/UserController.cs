using Microsoft.AspNetCore.Mvc;
using PollBack.Core.Interfaces.Services;
using PollBack.Core.Models;

namespace PollBack.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthenticationService authenticationService;

        public UserController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
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

                SignInResponse response = await authenticationService.SignInAsync(signInRequest);

                SetTokenCookie(response.RefreshToken);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RefreshToken()
        {
            string? refreshToken = Request.Cookies["refreshToken"];

            if (refreshToken == null)
                return BadRequest("Refresh token not found.");

            SignInResponse response = await authenticationService.RefreshTokenAsync(refreshToken, GetIpAddress());

            SetTokenCookie(response.RefreshToken);

            return Ok(response);

        }

        [HttpPost("[action]")]
        public async Task<IActionResult> RevokeToken(RevokeToken revokeToken)
        {
            string? token = revokeToken.Token ?? Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(token))
                return BadRequest("Refresh token not found.");

            await authenticationService.RevokeTokenAsync(token, GetIpAddress());

            return Ok();
        }


        private string GetIpAddress()
        {
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
                return Request.Headers["X-Forwarded-For"];
            else
            {
                string? ip = HttpContext.Connection.RemoteIpAddress?.MapToIPv4().ToString();

                return ip != null ? ip : string.Empty;
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
