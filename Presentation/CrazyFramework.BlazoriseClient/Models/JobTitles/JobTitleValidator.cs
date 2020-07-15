using FluentValidation;

namespace CrazyFramework.BlazoriseClient.Models.JobTitles
{
	public class JobTitleValidator : AbstractValidator<JobTitle>
	{
		public JobTitleValidator()
		{
			RuleFor(v => v.Name)
				.NotEmpty().WithMessage("Name is required.")
				.MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

			RuleFor(v => v.Description)
				.MaximumLength(1000).WithMessage("Description must not exceed 1000 characters.");
		}
	}
}