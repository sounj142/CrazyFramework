using CrazyFramework.Core.Services;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Services
{
	public class CurrentRequestContext : ICurrentRequestContext
	{
		private const string USER_ID_CLAIM = ClaimTypes.NameIdentifier;
		private const string USER_NAME_CLAIM = "email";

		public Guid? UserId { get; }
		public string UserName { get; }

		public int MaxTimeForRunningRequest { get; private set; }

		public CurrentRequestContext(IHttpContextAccessor httpContextAccessor)
		{
			MaxTimeForRunningRequest = 500;

			if (httpContextAccessor?.HttpContext?.User != null)
			{
				var userIdStr = httpContextAccessor.HttpContext.User.FindFirstValue(USER_ID_CLAIM);
				UserId = Guid.TryParse(userIdStr, out var userId) ? (Guid?)userId : null;

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