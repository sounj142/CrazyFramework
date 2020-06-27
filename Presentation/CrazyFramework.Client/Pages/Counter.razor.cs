using CrazyFramework.Client.Models;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.Client.Pages
{
	public partial class Counter
	{
		[Inject]
		public AppState appState { get; set; }

		private int currentCount;

		protected override void OnInitialized()
		{
			this.currentCount = appState.GetCurrentCount();
		}

		private void IncrementCount()
		{
			currentCount++;
			appState.UpdateCurrentCount(currentCount);
		}
	}
}