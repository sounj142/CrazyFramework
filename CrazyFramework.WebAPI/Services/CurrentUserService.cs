using CrazyFramework.Core.Interfaces.Services.Users;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CrazyFramework.WebAPI.Services
{
	public class CurrentUserService : ICurrentUserService
	{
		public CurrentUserService(IHttpContextAccessor httpContextAccessor)
		{
			var userIdStr = httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			UserId = Guid.TryParse(userIdStr, out var userId) ? (Guid?)userId : null;
		}

		public Guid? UserId { get; }
	}
}