using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Common
{
	public interface IDateTime
	{
		DateTime Now { get; }
		DateTime UtcNow { get; }
	}
}