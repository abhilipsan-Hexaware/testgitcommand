using System;
using System.Text;
using System.Threading.Tasks;
using DotnetTEST.Entities.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
namespace DotnetTEST.Api.Middleware
{
    public class UserContextMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly UserContext _userContext;

        public UserContextMiddleware(RequestDelegate next, UserContext userContext)
        {
            _userContext = userContext;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var authorizationHeaders = context.Request.Headers["Authorization"];
            _userContext.Email = "system";
            if (authorizationHeaders.Count > 0)
            {
                JwtSecurityTokenHandler jwt = new JwtSecurityTokenHandler();
                var jwtEncodedString = authorizationHeaders[0].Substring(7); // trim 'Bearer ' from the start since its just a prefix for the token string
                var token = jwt.ReadJwtToken(jwtEncodedString);
                _userContext.Email = token.Claims.First(c => c.Type == "emails").Value;
            }

            await _next(context);
        }
    }

    public static class UserContextMiddlewareExtensions
    {
        public static IApplicationBuilder UseUserContextMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserContextMiddleware>();
        }
    }

}