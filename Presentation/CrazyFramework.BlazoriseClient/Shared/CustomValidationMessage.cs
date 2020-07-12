using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Rendering;

namespace CrazyFramework.BlazoriseClient.Shared
{
	/// <summary>
	/// Displays a list of validation messages for a specified field within a cascaded <see cref="EditContext"/>.
	/// </summary>
	public class CustomValidationMessage<TValue> : ComponentBase, IDisposable
	{
		private EditContext _previousEditContext;
		private Expression<Func<TValue>> _previousFieldAccessor;
		private readonly EventHandler<ValidationStateChangedEventArgs> _validationStateChangedHandler;
		private FieldIdentifier _fieldIdentifier;

		/// <summary>
		/// Gets or sets a collection of additional attributes that will be applied to the created <c>div</c> element.
		/// </summary>
		[Parameter(CaptureUnmatchedValues = true)]
		public IReadOnlyDictionary<string, object> AdditionalAttributes { get; set; }

		[CascadingParameter]
		private EditContext CurrentEditContext { get; set; } = default!;

		/// <summary>
		/// Specifies the field for which validation messages should be displayed.
		/// </summary>
		[Parameter]
		public Expression<Func<TValue>> For { get; set; }

		[Parameter]
		public bool AddEmptyErrorDiv { get; set; } = true;

		/// <summary>`
		/// Constructs an instance of <see cref="ValidationMessage{TValue}"/>.
		/// </summary>
		public CustomValidationMessage()
		{
			_validationStateChangedHandler = (sender, eventArgs) => StateHasChanged();
		}

		/// <inheritdoc />
		protected override void OnParametersSet()
		{
			if (CurrentEditContext == null)
			{
				throw new InvalidOperationException($"{GetType()} requires a cascading parameter " +
					$"of type {nameof(EditContext)}. For example, you can use {GetType()} inside " +
					$"an {nameof(EditForm)}.");
			}

			if (For == null) // Not possible except if you manually specify T
			{
				throw new InvalidOperationException($"{GetType()} requires a value for the " +
					$"{nameof(For)} parameter.");
			}
			else if (For != _previousFieldAccessor)
			{
				_fieldIdentifier = FieldIdentifier.Create(For);
				_previousFieldAccessor = For;
			}

			if (CurrentEditContext != _previousEditContext)
			{
				DetachValidationStateChangedListener();
				CurrentEditContext.OnValidationStateChanged += _validationStateChangedHandler;
				_previousEditContext = CurrentEditContext;
			}
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			bool hasContent = false;
			foreach (var message in CurrentEditContext.GetValidationMessages(_fieldIdentifier))
			{
				builder.OpenElement(0, "div");
				builder.AddMultipleAttributes(1, AdditionalAttributes);
				builder.AddAttribute(2, "class", "validation-message");
				builder.AddContent(3, message);
				builder.CloseElement();
				hasContent = true;
			}

			if (AddEmptyErrorDiv && !hasContent)
			{
				builder.OpenElement(0, "div");
				builder.AddMultipleAttributes(1, AdditionalAttributes);
				builder.AddAttribute(2, "class", "validation-message");
				builder.AddContent(3, (MarkupString)"&nbsp;");
				builder.CloseElement();
			}
		}

		protected virtual void Dispose(bool disposing)
		{
		}

		void IDisposable.Dispose()
		{
			DetachValidationStateChangedListener();
			Dispose(disposing: true);
		}

		private void DetachValidationStateChangedListener()
		{
			if (_previousEditContext != null)
			{
				_previousEditContext.OnValidationStateChanged -= _validationStateChangedHandler;
			}
		}
	}
}