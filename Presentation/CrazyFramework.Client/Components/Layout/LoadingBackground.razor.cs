using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.Client.Components.Layout
{
	public partial class LoadingBackground
	{
		[Parameter]
		public bool ShowLogoBox { get; set; }

		[Parameter]
		public RenderFragment ChildContent { get; set; }
	}
}