﻿using System;

namespace CrazyFramework.App.Common.Exceptions
{
	public class ServiceException : FrameworkException
	{
		public ServiceException(string errorCode, string message) : base(errorCode, message)
		{
		}

		public ServiceException(string errorCode, string message, Exception innerException) : base(errorCode, message, innerException)
		{
		}
	}
}