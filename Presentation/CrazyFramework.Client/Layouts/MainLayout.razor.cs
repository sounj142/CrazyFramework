using System.Threading.Tasks;
using CrazyFramework.Client.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.Client.Layouts
{
	public partial class MainLayout
	{
		private bool _navMenuOpened = true;
		private bool _navMinified = false;
		public string bbDrawerClass { get; set; } = string.Empty;

		[Inject]
		public NavigationManager navigationManager { get; set; }

		[Inject]
		public AppState appState { get; set; }

		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		protected override async Task OnInitializedAsync()
		{
			// Uncomment to secure entire app  with no anonymous user access
			//navigationManager.NavigateTo(Shared.Settings.LoginPath);

			// Set Default landing page if you want for anonymous user.
			// Authenticated users are redirected after successful login.
			//navigationManager.NavigateTo("/");

			var user = (await authenticationStateTask).User;

			//// TODO1
			//if (user.Identity.IsAuthenticated)
			//{
			//	var profile = await appState.GetUserProfile();

			//	_navMenuOpened = profile.IsNavOpen;
			//	_navMinified = profile.IsNavMinified;
			//}
		}

		private void NavToggle()
		{
			_navMenuOpened = !_navMenuOpened;

			if (_navMenuOpened)
			{
				bbDrawerClass = "full";
			}
			else
			{
				bbDrawerClass = "closed";
			}

			StateHasChanged();
		}

		private void NavLock()
		{
			//Todo Lock Nav
		}

		private void NavMinify()
		{
			_navMinified = !_navMinified;

			if (!_navMenuOpened)
			{
				_navMinified = true;
			}

			if (_navMinified)
			{
				bbDrawerClass = "mini";
				_navMenuOpened = true;
			}
			else if (_navMenuOpened)
			{
				bbDrawerClass = "full";
			}

			_navMenuOpened = true;

			StateHasChanged();
		}
	}
}