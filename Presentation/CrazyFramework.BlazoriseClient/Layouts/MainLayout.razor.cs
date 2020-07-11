﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Layouts
{
	public partial class MainLayout
	{
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

		private bool siderVisible = true;
		private bool uiElementsVisible = true;

		private void ToggleSidebar()
		{
			siderVisible = !siderVisible;
			StateHasChanged();
		}

		[CascadingParameter]
		protected Theme Theme { get; set; }
	}
}