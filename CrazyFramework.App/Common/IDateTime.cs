using System;

namespace CrazyFramework.App.Common
{
	public interface IDateTime
	{
		DateTimeOffset Now { get; }
		DateTimeOffset UtcNow { get; }
	}
}