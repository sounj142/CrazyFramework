using CrazyFramework.App.Entities;
using CrazyFramework.App.Infrastructure.Repos;
using CrazyFramework.Dtos.JobTitles;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.JobTitles.Commands.UpdateJobTitle
{
	public class UpdateJobTitleCommand : UpdateJobTitleDto, IRequest
	{
		public class UpdateJobTitleCommandHandler : IRequestHandler<UpdateJobTitleCommand>
		{
			private readonly IJobTitleRepository _jobTitleRepository;
			private readonly ILogger<UpdateJobTitleCommand> _logger;

			public UpdateJobTitleCommandHandler(IJobTitleRepository jobTitleRepository, ILogger<UpdateJobTitleCommand> logger)
			{
				_jobTitleRepository = jobTitleRepository;
				_logger = logger;
			}

			public async Task<Unit> Handle(UpdateJobTitleCommand request, CancellationToken cancellationToken)
			{
				_logger.LogInformation("Updating job title {@Id}, {@Name}, {@Description}", request.Id, request.Name, request.Description);

				var jobTitle = new JobTitle(
					id: request.Id,
					name: request.Name,
					description: request.Description
				);

				await _jobTitleRepository.Update(jobTitle);
				return Unit.Value;
			}
		}
	}
}