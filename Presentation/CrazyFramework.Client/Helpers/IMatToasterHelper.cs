﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.Client.Shared;
using MatBlazor;

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