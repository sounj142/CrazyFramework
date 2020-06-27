using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyFramework.Client.Pages
{
	public partial class Index
	{
		private string ButtonState = "";

		private void Click()
		{
			ButtonState = "Clicked";
		}
	}
}