using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyFramework.BlazoriseClient.Pages.Tests
{
	public partial class TabsPage
	{
		private string selectedTabName1 = "home";
		private string selectedTabName2 = "profile";
		private string selectedTabName3 = "messages";
		private string selectedTabName4 = "settings";

		private void OnSelectedTabChanged1(string name)
		{
			selectedTabName1 = name;
		}

		private void OnSelectedTabChanged2(string name)
		{
			selectedTabName2 = name;
		}

		private void OnSelectedTabChanged3(string name)
		{
			selectedTabName3 = name;
		}

		private void OnSelectedTabChanged4(string name)
		{
			selectedTabName4 = name;
		}
	}
}