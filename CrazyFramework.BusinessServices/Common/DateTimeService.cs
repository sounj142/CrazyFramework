using CrazyFramework.App.Common;
using System;

namespace CrazyFramework.BusinessServices.Common
{
	// note: this class should be thread safe because its scope is Singleton
	public class DateTimeService : IDateTime
	{
		public DateTime Now => DateTime.Now;
		public DateTime UtcNow => DateTime.UtcNow;
	}
}