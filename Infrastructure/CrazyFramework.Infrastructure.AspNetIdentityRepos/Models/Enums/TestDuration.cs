using System.ComponentModel;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Enums
{
	internal enum TestDuration
	{
		[Description("3 hours")]
		ThreeHours = 1,

		[Description("One week")]
		OneWeek = 2
	}
}