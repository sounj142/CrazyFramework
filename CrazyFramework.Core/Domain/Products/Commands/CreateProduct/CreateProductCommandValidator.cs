using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Domain.Products.Commands.CreateProduct
{
	internal class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			// TODO: apply multiple languages to validation messages
			RuleFor(v => v.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(200).WithMessage("Name must not exceed 200 characters.");
		}
	}
}