using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CrazyFramework.Core.Domain.Results
{
	public class Result
	{
		protected Result(bool succeeded, Error error)
		{
			Succeeded = succeeded;
			Error = error;
		}

		public bool Succeeded { get; }

		public Error Error { get; }

		public static Result Success()
		{
			return new Result(true, null);
		}

		public static Result Failure(string errorId, string message)
		{
			return new Result(false, new Error(errorId, message));
		}
	}

	public class Result<T> : Result
	{
		public T Value { get; }

		protected Result(bool succeeded, T value, Error error) : base(succeeded, error)
		{
			Value = value;
		}

		public static Result<T> Success(T value)
		{
			return new Result<T>(true, value, null);
		}

		public new static Result Failure(string errorId, string message)
		{
			return new Result<T>(false, default, new Error(errorId, message));
		}
	}
}