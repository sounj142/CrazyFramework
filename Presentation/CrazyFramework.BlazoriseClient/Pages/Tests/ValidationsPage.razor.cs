using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazorise;
using CrazyFramework.BlazoriseClient.Models;

namespace CrazyFramework.BlazoriseClient.Pages.Tests
{
	public partial class ValidationsPage
	{
		protected Validations validations;

		protected Validations annotationsValidations;

		protected User user = new User();
		protected User manualUser = new User();

		protected string password;

		private void ValidatePassword(ValidatorEventArgs e)
		{
			e.Status = Convert.ToString(e.Value)?.Length >= 6 ? ValidationStatus.Success : ValidationStatus.Error;
		}

		private void ValidatePassword2(ValidatorEventArgs e)
		{
			var password2 = Convert.ToString(e.Value);

			if (password2?.Length < 6)
			{
				e.Status = ValidationStatus.Error;
				e.ErrorText = "Password must be at least 6 characters long!";
			}
			else if (password2 != password)
			{
				e.Status = ValidationStatus.Error;
			}
			else
			{
				e.Status = ValidationStatus.Success;
			}
		}

		private void ValidateCheck(ValidatorEventArgs e)
		{
			e.Status = Convert.ToBoolean(e.Value) ? ValidationStatus.Success : ValidationStatus.Error;
		}

		private void ValidateSelect(ValidatorEventArgs e)
		{
			var selectedValue = e.Value == null ? 0 : Convert.ToInt32(e.Value);
			e.Status = selectedValue != 0 ? ValidationStatus.Success : ValidationStatus.Error;
		}

		private void ValidateDateOfBirth(ValidatorEventArgs e)
		{
			var date = e.Value as DateTime?;

			if (date == null)
			{
				e.Status = ValidationStatus.Error;
			}
			else if (date?.Year < 1900)
			{
				e.Status = ValidationStatus.Error;
				e.ErrorText = "Date cannot be less then 01.01.1900!";
			}
			else
			{
				e.Status = ValidationStatus.Success;
			}
		}

		private void Submit()
		{
			if (validations.ValidateAll())
			{
				validations.ClearAll();
			}
		}

		private void AnnotationsSubmit()
		{
			if (annotationsValidations.ValidateAll())
			{
				annotationsValidations.ClearAll();
			}
		}
	}
}