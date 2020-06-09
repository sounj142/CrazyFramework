using Xunit;

// because we are doing integration tests against a real database. If we allow running multiple tests in parallel, the action
// in a test can affect another tests and bring unexpected results.
// so, we use this attribute to turn off parallelism inside the assembly
// https://xunit.net/docs/running-tests-in-parallel.html
[assembly: CollectionBehavior(DisableTestParallelization = true)]

namespace CrazyFramework.WebAPI.IntegrationTests
{
	internal static class TestConstants
	{
		public static readonly string ContentType = "application/json; charset=utf-8";
		public static readonly string DbConnection = "Server=(localdb)\\MSSQLLocalDB;Database=CrazyFrameworkDb_Test;Trusted_Connection=True;MultipleActiveResultSets=true";
		public static readonly string ProductApiBaseUrl = "/api/Products";
		public static readonly string TestToken = "Test-Token";
	}
}