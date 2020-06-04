using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Interfaces.Services.Users
{
	public interface ICurrentUserService
	{
		Guid? UserId { get; }
	}
}