using System;

namespace CrazyFramework.App.BusinessServices
{
	public interface ICurrentRequestContext
	{
		string UserId { get; }
		string UserName { get; }
		int MaxTimeForRunningRequest { get; }
	}
}