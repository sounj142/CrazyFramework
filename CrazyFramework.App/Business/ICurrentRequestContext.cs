namespace CrazyFramework.App.BusinessServices
{
	public interface ICurrentRequestContext
	{
		string GetCurrentUserId();

		string GetCurrentUserName();
	}
}