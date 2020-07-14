using CrazyFramework.App.Entities;
using CrazyFramework.App.Infrastructure.Repos;
using CrazyFramework.Dtos.JobTitles;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.JobTitles.Commands.CreateJobTitle
{
	public class CreateJobTitleCommand : CreateJobTitleDto, IRequest<Guid>
	{
		public class CreateJobTitleCommandHandler : IRequestHandler<CreateJobTitleCommand, Guid>
		{
			private readonly IJobTitleRepository _jobTitleRepository;
			private readonly ILogger<CreateJobTitleCommandHandler> _logger;

			public CreateJobTitleCommandHandler(IJobTitleRepository jobTitleRepository, ILogger<CreateJobTitleCommandHandler> logger)
			{
				_jobTitleRepository = jobTitleRepository;
				_logger = logger;
			}

			public async Task<Guid> Handle(CreateJobTitleCommand request, CancellationToken cancellationToken)
			{
				_logger.LogInformation("Creating job title {@Name}, {@Description}", request.Name, request.Description);

				var jobTitle = new JobTitle(
					id: Guid.NewGuid(),
					name: request.Name,
					description: request.Description);

				await _jobTitleRepository.Create(jobTitle);
				return jobTitle.Id;
			}
		}
	}
}