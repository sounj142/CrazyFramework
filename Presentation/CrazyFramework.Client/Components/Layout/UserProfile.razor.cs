using System.Threading.Tasks;
using CrazyFramework.Client.Models.Users;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.Client.Components.Layout
{
	public partial class UserProfile
	{
		public bool IsLoggedIn = false;
		private UserInfo userInfo;

		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		protected override async Task OnParametersSetAsync()
		{
			userInfo = null;
			var user = (await authenticationStateTask).User;

			if (user.Identity.IsAuthenticated)
			{
				userInfo = new UserInfo
				{
					UserName = user.Identity.Name
				};
			}
		}
	}
}