using System;

namespace CrazyFramework.App.BusinessServices
{
	public interface ICurrentRequestContext
	{
		Guid? UserId { get; }
		string UserName { get; }
		int MaxTimeForRunningRequest { get; }
	}
}