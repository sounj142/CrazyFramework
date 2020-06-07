using FluentValidation;

namespace CrazyFramework.App.Handlers.Products.Commands.CreateProduct
{
	public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
	{
		public CreateProductCommandValidator()
		{
			// TODO: apply multiple languages to validation messages
			RuleFor(v => v.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(200).WithMessage("Name must not exceed 200 characters.");

			RuleFor(v => v.Price)
				.GreaterThan(0).WithMessage("Price must be greater than 0.");
		}
	}
}