using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Domain.Results
{
	public class Error
	{
		public string ErrorId { get; }

		// TODO: apply multi languages
		public string Message { get; }

		public Error(string errorId, string message)
		{
			ErrorId = errorId;
			Message = message;
		}
	}
}