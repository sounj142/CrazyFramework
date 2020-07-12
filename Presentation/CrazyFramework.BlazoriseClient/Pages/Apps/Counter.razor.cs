using Blazorise.Snackbar;
using CrazyFramework.BlazoriseClient.Models;
using CrazyFramework.BlazoriseClient.Shared;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Pages.Apps
{
	[Authorize]
	public partial class Counter
	{
		[Inject]
		public AppState appState { get; set; }

		[Inject]
		public NotificationService notificationService { get; set; }

		private int currentCount;

		protected override void OnInitialized()
		{
			currentCount = appState.Counter.CurrentCount;
		}

		private void IncrementCount()
		{
			currentCount++;
			appState.Counter.UpdateCurrentCount(currentCount);
			notificationService.ShowSuccessSnackbar("<b>sdsdsd sdd</b> sdsd<br> asdaas");
		}
	}
}