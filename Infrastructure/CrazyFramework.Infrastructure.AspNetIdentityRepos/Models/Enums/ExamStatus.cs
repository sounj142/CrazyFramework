namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Enums
{
	internal enum ExamStatus
	{
		Initial = 10, // Associate a test with candidate but email hasn't been sent out.
		SentFail = 11,
		Sent = 20, // Sent out but not start.
		Testing = 30, // After start. Keep this status during testing time.
		PendingForEvaluation = 40, //After time-up.
		Evaluated = 50,
		Cancelled = 80
	}
}