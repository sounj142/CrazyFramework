using System;
using Blazorise.Snackbar;
using CrazyFramework.BlazoriseClient.Shared;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Components.Common
{
	public partial class GlobalSnackbar : IDisposable
	{
		protected Snackbar snackbar;
		protected SnackbarColor color = SnackbarColor.Info;
		protected MarkupString message = (MarkupString)string.Empty;

		[Inject]
		public NotificationService notificationService { get; set; }

		protected override void OnInitialized()
		{
			notificationService.SnackbarNotify += ToggleSnackbar;
		}

		private void ToggleSnackbar(SnackbarColor color, string message)
		{
			this.color = color;
			this.message = (MarkupString)message;

			snackbar.Show();
			StateHasChanged();
		}

		public void Dispose()
		{
			notificationService.SnackbarNotify -= ToggleSnackbar;
		}
	}
}