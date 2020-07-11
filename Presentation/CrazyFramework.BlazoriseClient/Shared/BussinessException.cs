using System;
using System.Collections.Generic;
using System.Linq;

namespace CrazyFramework.BlazoriseClient.Shared
{
	public class BussinessException : Exception
	{
		private readonly IDictionary<string, string[]> _errors;

		public BussinessException(string message) : base(message ?? string.Empty)
		{
			_errors = new Dictionary<string, string[]>();
		}

		public BussinessException(IDictionary<string, string[]> errors) : base(string.Empty)
		{
			_errors = errors ?? new Dictionary<string, string[]>();
		}

		public BussinessException(string message, IDictionary<string, string[]> errors) : base(message ?? string.Empty)
		{
			_errors = errors ?? new Dictionary<string, string[]>();
		}

		public string GetErrorMessage()
		{
			var msg = string.Join(" | ", _errors.Values.SelectMany(e => e));
			msg = string.IsNullOrEmpty(Message) ? msg : string.IsNullOrEmpty(msg) ? Message : $"[{Message}]: {msg}";
			return string.IsNullOrEmpty(msg) ? "Unknown error" : msg;
		}
	}
}