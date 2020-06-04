using CrazyFramework.Core.Interfaces.Services.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Services.Common
{
	public class DateTimeService : IDateTimeService
	{
		public DateTime Now => DateTime.Now;
		public DateTime UtcNow => DateTime.UtcNow;
	}
}