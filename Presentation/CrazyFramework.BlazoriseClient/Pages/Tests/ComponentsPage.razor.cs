using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrazyFramework.BlazoriseClient.Pages.Tests
{
	public partial class ComponentsPage
	{
		public class MySelectModel
		{
			public int MyValueField { get; set; }
			public string MyTextField { get; set; }
		}

		private static string[] Countries = { "Albania", "Andorra", "Armenia", "Austria", "Azerbaijan", "Belarus", "Belgium", "Bosnia & Herzegovina", "Bulgaria", "Croatia", "Cyprus", "Czech Republic", "Denmark", "Estonia", "Finland", "France", "Georgia", "Germany", "Greece", "Hungary", "Iceland", "Ireland", "Italy", "Kosovo", "Latvia", "Liechtenstein", "Lithuania", "Luxembourg", "Macedonia", "Malta", "Moldova", "Monaco", "Montenegro", "Netherlands", "Norway", "Poland", "Portugal", "Romania", "Russia", "San Marino", "Serbia", "Slovakia", "Slovenia", "Spain", "Sweden", "Switzerland", "Turkey", "Ukraine", "United Kingdom", "Vatican City" };
		private IEnumerable<MySelectModel> myDdlData = Enumerable.Range(1, Countries.Length).Select(x => new MySelectModel { MyTextField = Countries[x - 1], MyValueField = x });

		private object selectedListValue { get; set; } = 3;
		private object selectedDropValue { get; set; } = 2;
		private object selectedSearchValue { get; set; }

		private void MyListValueChangedHandler(object newValue)
		{
			selectedListValue = newValue;
		}

		private void MyDropValueChangedHandler(object newValue)
		{
			selectedDropValue = newValue;
		}

		private void MySearchHandler(object newValue)
		{
			selectedSearchValue = newValue;
		}
	}
}