using CrazyFramework.Core.Services;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Core.Common.Behaviours
{
	public class RequestPerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
	{
		private readonly Stopwatch _timer;
		private readonly ILogger<TRequest> _logger;
		private readonly ICurrentRequestContext _currentRequestContext;

		public RequestPerformanceBehaviour(
			ILogger<TRequest> logger,
			ICurrentRequestContext currentRequestContext)
		{
			_timer = new Stopwatch();

			_logger = logger;
			_currentRequestContext = currentRequestContext;
		}

		public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
		{
			_timer.Start();

			var response = await next();

			_timer.Stop();

			var elapsedMilliseconds = _timer.ElapsedMilliseconds;

			if (elapsedMilliseconds > 500)
			{
				var requestName = typeof(TRequest).Name;

				_logger.LogWarning("CleanArchitecture Long Running Request: {Name} ({ElapsedMilliseconds} milliseconds) {@UserId} {@Request}",
					requestName, elapsedMilliseconds, _currentRequestContext.UserId, _currentRequestContext.UserName, request);
			}

			return response;
		}
	}
}