using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace CrazyFramework.BlazoriseClient.Components.Common
{
	public partial class ThemeColorSelector
	{
		[Parameter]
		public string Value
		{
			get => _value;
			set
			{
				if (value == _value)
				{
					return;
				}
				_value = value;
				this.StateHasChanged();
				ValueChanged.InvokeAsync(value);
			}
		}

		private string ClassNames(string value)
			=> $"demo-theme-color-item{(value == Value ? " selected" : "")}";

		[Parameter]
		public EventCallback<string> ValueChanged { get; set; }

		private string _value;

		private Task OnClick(string value)
		{
			Value = value;

			return Task.CompletedTask;
		}
	}
}