using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.BlazoriseClient.Layouts
{
	public partial class MainLayout
	{
		[CascadingParameter]
		private Task<AuthenticationState> authenticationStateTask { get; set; }

		protected bool siderVisible = true;
		protected bool uiElementsVisible = true;

		[CascadingParameter]
		protected Theme Theme { get; set; }

		protected UserInfo userInfo = null;

		protected override async Task OnParametersSetAsync()
		{
			var user = (await authenticationStateTask).User;

			if (user.Identity.IsAuthenticated)
			{
				userInfo = new UserInfo
				{
					UserName = user.Identity.Name
				};
			}
		}

		private void OnThemeEnabledChanged(bool value)
		{
			if (Theme == null)
				return;

			Theme.Enabled = value;

			Theme.ThemeHasChanged();
		}

		private void OnGradientChanged(bool value)
		{
			if (Theme == null)
				return;

			Theme.IsGradient = value;

			//if ( Theme.GradientOptions == null )
			//    Theme.GradientOptions = new GradientOptions();

			//Theme.GradientOptions.BlendPercentage = 80;

			Theme.ThemeHasChanged();
		}

		private void OnRoundedChanged(bool value)
		{
			if (Theme == null)
				return;

			Theme.IsRounded = value;

			Theme.ThemeHasChanged();
		}

		private void OnThemeColorChanged(string value)
		{
			if (Theme == null)
				return;

			if (Theme.ColorOptions == null)
				Theme.ColorOptions = new ThemeColorOptions();

			if (Theme.BackgroundOptions == null)
				Theme.BackgroundOptions = new ThemeBackgroundOptions();

			if (Theme.TextColorOptions == null)
				Theme.TextColorOptions = new ThemeTextColorOptions();

			Theme.ColorOptions.Primary = value;
			Theme.BackgroundOptions.Primary = value;
			Theme.TextColorOptions.Primary = value;

			if (Theme.InputOptions == null)
				Theme.InputOptions = new ThemeInputOptions();

			//Theme.InputOptions.Color = value;
			Theme.InputOptions.CheckColor = value;
			Theme.InputOptions.SliderColor = value;

			Theme.ThemeHasChanged();
		}

		private void ToggleSidebar()
		{
			siderVisible = !siderVisible;
			StateHasChanged();
		}
	}
}