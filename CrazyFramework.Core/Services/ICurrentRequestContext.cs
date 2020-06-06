using System;

namespace CrazyFramework.Core.Services
{
	public interface ICurrentRequestContext
	{
		Guid? UserId { get; }
		string UserName { get; }
		int MaxTimeForRunningRequest { get; }
	}
}