using System;

namespace CrazyFramework.Core.Common.Exceptions
{
	public class NotFoundException : FrameworkException
	{
		public NotFoundException(string entityName, Guid key) : this(ErrorCodes.ErrorCode.ENTITY_NOT_FOUND, entityName, key)
		{
		}

		// TODO: apply multi languages
		public NotFoundException(string errorCode, string entityName, Guid key) : base(errorCode,
			$"Entity \"{entityName}\" ({key}) was not found.")
		{
		}
	}
}