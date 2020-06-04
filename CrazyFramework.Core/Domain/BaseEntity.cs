using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Domain
{
	public abstract class BaseEntity
	{
		public Guid Id { get; set; }
	}
}