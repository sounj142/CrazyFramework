namespace CrazyFramework.Infrastructure.Repos
{
	/// <summary>
	/// Internal errors, only use for log and troubleshooting error, don't check these error codes in other layer. It's private in Repository
	/// </summary>
	internal class ErrorCode
	{
		public static readonly string REPO001 = "REPO001";
		public static readonly string REPO002 = "REPO002";
	}
}