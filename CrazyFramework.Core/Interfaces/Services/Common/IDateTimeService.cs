using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Interfaces.Services.Common
{
	public interface IDateTimeService
	{
		DateTime Now { get; }
		DateTime UtcNow { get; }
	}
}