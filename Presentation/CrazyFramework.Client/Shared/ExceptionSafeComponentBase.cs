using System;
using System.Threading.Tasks;
using MatBlazor;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace CrazyFramework.Client.Shared
{
	public abstract class ExceptionSafeComponentBase : ComponentBase
	{
		[Inject]
		public IMatToaster matToaster { get; set; }

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			TryCatch(() => base.BuildRenderTree(builder));
		}

		protected override void OnAfterRender(bool firstRender)
		{
			TryCatch(() => base.OnAfterRender(firstRender));
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await TryCatchAsync(() => base.OnAfterRenderAsync(firstRender));
		}

		protected override void OnInitialized()
		{
			TryCatch(base.OnInitialized);
		}

		protected override Task OnInitializedAsync()
		{
			return TryCatchAsync(base.OnInitializedAsync);
		}

		protected override void OnParametersSet()
		{
			TryCatch(base.OnParametersSet);
		}

		protected override Task OnParametersSetAsync()
		{
			return TryCatchAsync(base.OnParametersSetAsync);
		}

		public override async Task SetParametersAsync(ParameterView parameters)
		{
			await TryCatchAsync(() => base.SetParametersAsync(parameters));
		}

		protected void TryCatch(Action action)
		{
			try
			{
				action();
			}
			catch (BussinessException ex)
			{
				matToaster.Add(
					message: ex.GetErrorMessage(),
					type: MatToastType.Danger,
					title: "Operation Failed");
			}
		}

		protected async Task TryCatchAsync(Func<Task> action)
		{
			try
			{
				await action();
			}
			catch (BussinessException ex)
			{
				matToaster.Add(
					message: ex.GetErrorMessage(),
					type: MatToastType.Danger,
					title: "Operation Failed");
			}
		}
	}
}