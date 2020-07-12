using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;

namespace CrazyFramework.BlazoriseClient.Shared
{
	public static class ErrorDisplayingHelper
	{
		public static void CatchAndDisplayErrors(Action action)
		{
			try
			{
				action();
			}
			catch (AccessTokenNotAvailableException exception)
			{
				exception.Redirect();
			}
			catch (BussinessException ex)
			{
				NotificationService.Instance.ShowDangerSnackbar(ex.GetErrorMessage());
			}
		}

		public static async Task CatchAndDisplayErrors(Func<Task> action)
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
				NotificationService.Instance.ShowDangerSnackbar(ex.GetErrorMessage());
			}
		}
	}
}