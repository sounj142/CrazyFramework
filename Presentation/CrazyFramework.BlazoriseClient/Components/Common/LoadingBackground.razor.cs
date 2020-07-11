using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Components.Common
{
	public partial class LoadingBackground
	{
		[Parameter]
		public bool ShowLogoBox { get; set; }

		[Parameter]
		public RenderFragment ChildContent { get; set; }
	}
}