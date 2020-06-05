using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Services
{
	public interface ICurrentRequestContext
	{
		Guid? UserId { get; }
		string UserName { get; }
	}
}