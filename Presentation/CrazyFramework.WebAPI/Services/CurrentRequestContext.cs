using CrazyFramework.App.BusinessServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CrazyFramework.WebAPI.Services
{
	public class CurrentRequestContext : ICurrentRequestContext
	{
		private const string USER_ID_CLAIM = "sub";
		private const string USER_NAME_CLAIM = "email";
		private readonly IHttpContextAccessor _httpContextAccessor;

		public string GetCurrentUserId() => _httpContextAccessor.HttpContext?.User?.FindFirstValue(USER_ID_CLAIM);

		public string GetCurrentUserName() => _httpContextAccessor.HttpContext?.User?.FindFirstValue(USER_NAME_CLAIM);

		public CurrentRequestContext(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccessor = httpContextAccessor;
		}
	}
}