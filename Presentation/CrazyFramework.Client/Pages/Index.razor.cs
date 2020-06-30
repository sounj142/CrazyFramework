using Microsoft.AspNetCore.Authorization;

namespace CrazyFramework.Client.Pages
{
	[Authorize]
	public partial class Index
	{
		private string ButtonState = "";

		private void Click()
		{
			ButtonState = "Clicked";
		}
	}
}