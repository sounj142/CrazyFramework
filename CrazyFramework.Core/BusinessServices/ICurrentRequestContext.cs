using System;

namespace CrazyFramework.Core.BusinessServices
{
	public interface ICurrentRequestContext
	{
		Guid? UserId { get; }
		string UserName { get; }
		int MaxTimeForRunningRequest { get; }
	}
}