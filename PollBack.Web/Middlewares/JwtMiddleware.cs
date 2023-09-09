using PollBack.Core.Interfaces.Services;
using System.Security.Claims;

namespace PollBack.Web.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IJwtTokenService jwtTokenService;

        public JwtMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService)
        {     
            this.next = next;
            this.jwtTokenService = jwtTokenService;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            string? token = httpContext.Request.Headers["Authorization"]
                .FirstOrDefault()?
                .Split(" ")
                .Last();

            int? userId = token != null ? jwtTokenService.ValidateJwtToken(token) : null;

            if(userId != null)
            {
                string? strUserId = userId.ToString();

                if(strUserId != null)
                {
                    httpContext.User.Identities.FirstOrDefault()?.AddClaim(new Claim("UserId", strUserId));
                }
            }

            await next(httpContext);
        }
    }
}
