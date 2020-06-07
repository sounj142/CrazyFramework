using System;

namespace CrazyFramework.App.Common
{
	public interface IDateTime
	{
		DateTime Now { get; }
		DateTime UtcNow { get; }
	}
}