using CrazyFramework.App.Infrastructure.Repos;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.JobTitles.Commands.DeleteJobTitle
{
	public class DeleteJobTitleCommand : IRequest
	{
		public Guid Id { get; set; }

		public class DeleteJobTitleCommandHandler : IRequestHandler<DeleteJobTitleCommand>
		{
			private readonly IJobTitleRepository _jobTitleRepository;
			private readonly ILogger<DeleteJobTitleCommand> _logger;

			public DeleteJobTitleCommandHandler(IJobTitleRepository jobTitleRepository, ILogger<DeleteJobTitleCommand> logger)
			{
				_jobTitleRepository = jobTitleRepository;
				_logger = logger;
			}

			public async Task<Unit> Handle(DeleteJobTitleCommand request, CancellationToken cancellationToken)
			{
				_logger.LogInformation("Deleting job title {@Id}", request.Id);

				await _jobTitleRepository.Delete(request.Id);
				return Unit.Value;
			}
		}
	}
}