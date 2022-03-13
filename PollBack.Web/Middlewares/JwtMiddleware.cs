using MediatR;
using PollBack.Core.Interfaces.Services;
using PollBack.Core.UserAggregate.Queries;

namespace PollBack.Web.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate next;
        private readonly IJwtTokenService jwtTokenService;
        private readonly IMediator mediator;

        public JwtMiddleware(RequestDelegate next, IJwtTokenService jwtTokenService, IMediator mediator)
        {     
            this.next = next;
            this.jwtTokenService = jwtTokenService;
            this.mediator = mediator;
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
                httpContext.Items["User"] = await mediator.Send(new GetUserByIdQuery(userId.Value));
            }

            await next(httpContext);

        }
    }
}
