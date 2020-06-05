namespace CrazyFramework.Core.Common.ErrorCodes
{
	/// <summary>
	/// Common errors that should be thrown by Repository. Other layers can catch exceptions and check these errors to know what happened
	/// </summary>
	public static class RepositoryErrorCode
	{
		public static class Product
		{
			public static class Update
			{
				public static readonly string PRODUCT_MISSING = "RepositoryErrorCode.Product.Update.PRODUCT_MISSING";
			}
		}
	}
}