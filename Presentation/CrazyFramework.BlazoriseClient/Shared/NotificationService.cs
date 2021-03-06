﻿using System;
using Blazorise.Snackbar;

namespace CrazyFramework.BlazoriseClient.Shared
{
	public class NotificationService
	{
		#region Snackbar

		public event Action<SnackbarColor, string> SnackbarNotify;

		public void ShowSuccessSnackbar(string message)
		{
			SnackbarNotify?.Invoke(SnackbarColor.Success, message);
		}

		public void ShowDangerSnackbar(string message)
		{
			SnackbarNotify?.Invoke(SnackbarColor.Danger, message);
		}

		public void ShowWarningSnackbar(string message)
		{
			SnackbarNotify?.Invoke(SnackbarColor.Warning, message);
		}

		public void ShowInfoSnackbar(string message)
		{
			SnackbarNotify?.Invoke(SnackbarColor.Info, message);
		}

		#endregion Snackbar
	}
}