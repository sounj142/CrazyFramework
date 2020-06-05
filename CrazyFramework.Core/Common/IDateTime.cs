using System;

namespace CrazyFramework.Core.Common
{
	public interface IDateTime
	{
		DateTime Now { get; }
		DateTime UtcNow { get; }
	}
}