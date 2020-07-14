using FluentValidation;

namespace CrazyFramework.App.Handlers.JobTitles.Commands.UpdateJobTitle
{
	public class UpdateJobTitleCommandValidator : AbstractValidator<UpdateJobTitleCommand>
	{
		public UpdateJobTitleCommandValidator()
		{
			RuleFor(v => v.Id)
				.NotEmpty().WithMessage("Id is required.");

			RuleFor(v => v.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

			RuleFor(v => v.Description)
				.MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
		}
	}
}