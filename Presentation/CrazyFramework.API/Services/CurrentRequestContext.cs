using CrazyFramework.App.BusinessServices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CrazyFramework.API.Services
{
	public class CurrentRequestContext : ICurrentRequestContext
	{
		private const string USER_ID_CLAIM = "sub";
		private const string USER_NAME_CLAIM = "email";

		public string UserId { get; }
		public string UserName { get; }

		public int MaxTimeForRunningRequest { get; private set; }

		public CurrentRequestContext(IHttpContextAccessor httpContextAccessor)
		{
			MaxTimeForRunningRequest = 500;

			if (httpContextAccessor?.HttpContext?.User != null)
			{
				UserId = httpContextAccessor.HttpContext.User.FindFirstValue(USER_ID_CLAIM);

				UserName = httpContextAccessor.HttpContext.User.FindFirstValue(USER_NAME_CLAIM);
			}
		}

		/// <summary>
		/// In some special request (export pdf, reporting ...), maybe we need to increase this maximum value
		/// </summary>
		public void ChangeMaxTimeForRunningRequest(int value)
		{
			MaxTimeForRunningRequest = value;
		}
	}
}