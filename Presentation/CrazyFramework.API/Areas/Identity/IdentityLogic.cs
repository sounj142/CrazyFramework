using System;
using System.Linq;
using CrazyFramework.App.BusinessServices;
using CrazyFramework.App.Entities;

namespace CrazyFramework.API.Areas.Identity
{
	public interface IIdentityLogic
	{
		bool IsLegalToDeleteUser(string userName);
	}

	internal class IdentityLogic : IIdentityLogic
	{
		private readonly AppSettings _appSettings;
		private readonly ICurrentRequestContext _requestContext;

		public IdentityLogic(AppSettings appSettings, ICurrentRequestContext requestContext)
		{
			_appSettings = appSettings;
			_requestContext = requestContext;
		}

		public bool IsLegalToDeleteUser(string userName)
		{
			var currentUser = _requestContext.GetCurrentUserName();

			return !currentUser.Equals(userName, StringComparison.OrdinalIgnoreCase) &&
				!_appSettings.SuperUsers.Any(u => userName.Equals(u, StringComparison.OrdinalIgnoreCase));
		}
	}
}