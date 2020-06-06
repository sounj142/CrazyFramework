using FluentValidation;

namespace CrazyFramework.Core.Business.Products.Commands.UpdateProduct
{
	public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
	{
		public UpdateProductCommandValidator()
		{
			RuleFor(v => v.Id)
				.NotEmpty().WithMessage("Id is required.");

			// TODO: apply multiple languages to validation messages
			RuleFor(v => v.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

			RuleFor(v => v.Price)
				.GreaterThan(0).WithMessage("Price must be greater than 0.");
		}
	}
}