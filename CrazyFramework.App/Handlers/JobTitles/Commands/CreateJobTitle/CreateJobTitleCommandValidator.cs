using FluentValidation;

namespace CrazyFramework.App.Handlers.JobTitles.Commands.CreateJobTitle
{
	public class CreateJobTitleCommandValidator : AbstractValidator<CreateJobTitleCommand>
	{
		public CreateJobTitleCommandValidator()
		{
			RuleFor(v => v.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

			RuleFor(v => v.Description)
				.MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
		}
	}
}