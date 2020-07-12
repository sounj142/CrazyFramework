using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace CrazyFramework.BlazoriseClient.Shared
{
	public class ValidationChangingSupport : ComponentBase, IDisposable
	{
		[CascadingParameter]
		private EditContext CurrentEditContext { get; set; } = default!;

		[Parameter]
		public bool ValidateAfterTheFirstSubmit { get; set; } = true;

		private EditContext _previousEditContext;
		private bool validationRequested;

		protected override void OnParametersSet()
		{
			if (CurrentEditContext != _previousEditContext)
			{
				DetachValidationRequestedListener();
				CurrentEditContext.OnValidationRequested += OnValidationRequestedHandler;
				_previousEditContext = CurrentEditContext;
			}
		}

		public void OnModelChanged()
		{
			if (validationRequested || !ValidateAfterTheFirstSubmit)
			{
				CurrentEditContext.Validate();
			}
		}

		public void OnValidationRequestedHandler(object sender, ValidationRequestedEventArgs e)
		{
			validationRequested = true;
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		void IDisposable.Dispose()
		{
			DetachValidationRequestedListener();
			Dispose(disposing: true);
		}

		private void DetachValidationRequestedListener()
		{
			if (_previousEditContext != null)
			{
				_previousEditContext.OnValidationRequested -= OnValidationRequestedHandler;
			}
		}
	}
}