using FluentValidation.Results;
using System.Collections.Generic;
using System.Linq;

namespace CrazyFramework.Core.Common.Exceptions
{
	public class ValidationException : FrameworkException
	{
		public IDictionary<string, string[]> Failures { get; } = new Dictionary<string, string[]>();

		// TODO: apply multi languages
		public ValidationException() : base(ErrorCodes.ErrorCode.VALIDATOR_ERROR, "One or more validation failures have occurred.")
		{
		}

		public ValidationException(IList<ValidationFailure> failures) : this()
		{
			ProcessValidationFailure(failures);
		}

		private void ProcessValidationFailure(IList<ValidationFailure> failures)
		{
			var failureGroups = failures
				.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

			foreach (var failureGroup in failureGroups)
			{
				var propertyName = failureGroup.Key;
				var propertyFailures = failureGroup.ToArray();

				Failures.Add(propertyName, propertyFailures);
			}
		}
	}
}