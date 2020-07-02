using System;
using System.Threading.Tasks;
using CrazyFramework.Client.Shared;
using MatBlazor;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CrazyFramework.Client.Helpers
{
	public static class IMatToasterHelper
	{
		public static void CatchAndDisplayErrors(this IMatToaster matToaster, Action action)
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

		public static async Task CatchAndDisplayErrors(this IMatToaster matToaster, Func<Task> action)
		{
			try
			{
				await action();
			}
			catch (AccessTokenNotAvailableException exception)
			{
				exception.Redirect();
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