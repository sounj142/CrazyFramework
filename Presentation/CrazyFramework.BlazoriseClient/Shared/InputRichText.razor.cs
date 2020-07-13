using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CrazyFramework.BlazoriseClient.Shared
{
	public partial class InputRichText : IDisposable
	{
		private string _id;

		[Parameter]
		public string Id
		{
			get => _id ?? $"CKEditor_{uid}";
			set => _id = value;
		}

		[Inject]
		public IJSRuntime JSRuntime { get; set; }

		private readonly string uid = Guid.NewGuid().ToString().ToLower().Replace("-", "");

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			if (firstRender)
				await JSRuntime.InvokeVoidAsync("CKEditorInterop.init", Id, DotNetObjectReference.Create(this));

			await base.OnAfterRenderAsync(firstRender);
		}

		[JSInvokable]
		public Task EditorDataChanged(string data)
		{
			CurrentValue = data;
			StateHasChanged();
			return Task.CompletedTask;
		}

		public void Dispose()
		{
			JSRuntime.InvokeVoidAsync("CKEditorInterop.destroy", Id);
		}
	}
}