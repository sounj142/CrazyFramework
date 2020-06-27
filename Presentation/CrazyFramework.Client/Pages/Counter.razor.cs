using CrazyFramework.Client.Models;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.Client.Pages
{
	public partial class Counter
	{
		[Inject]
		public AppState appState { get; set; }

		private void IncrementCount()
		{
			appState.CurrentCount++;
		}
	}
}