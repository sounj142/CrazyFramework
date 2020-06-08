using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace CrazyFramework.SpaApp.Angular.Helpers
{
	public class RequiredAuthenticationMiddleware
	{
		private readonly RequestDelegate _next;

		public RequiredAuthenticationMiddleware(RequestDelegate next)
		{
			_next = next;
		}

		public async Task Invoke(HttpContext context)
		{
			if (!context.User.Identity.IsAuthenticated)
			{
				await context.ChallengeAsync("oidc");
			}
			else
			{
				await _next(context);
			}
		}
	}

	public static class RequiredAuthenticationMiddlewareExtensions
	{
		public static IApplicationBuilder RequiredAuthentication(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<RequiredAuthenticationMiddleware>();
		}
	}
}