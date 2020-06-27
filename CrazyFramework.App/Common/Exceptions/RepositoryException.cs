using System;

namespace CrazyFramework.App.Common.Exceptions
{
	public class RepositoryException : FrameworkException
	{
		public RepositoryException(string errorCode, string message) : base(errorCode, message)
		{
		}

		public RepositoryException(string errorCode, string message, Exception innerException) : base(errorCode, message, innerException)
		{
		}
	}
}