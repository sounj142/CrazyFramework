using System.ComponentModel;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Enums
{
	internal enum EvaluationFieldType
	{
		[Description("Score (0-5)")]
		Comment = 0,

		[Description("Comments")]
		CodexSnippet = 1,

		[Description("Codex Snippet")]
		Score = 2,

		Submitted = 3,
	}
}