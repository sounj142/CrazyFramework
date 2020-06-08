using Microsoft.AspNetCore.Builder;

namespace CrazyFramework.WebAPI.Helpers
{
	public static class ExceptionHandlerMiddlewareExtensions
	{
		public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
		{
			return builder.UseMiddleware<ExceptionHandlerMiddleware>();
		}
	}
}