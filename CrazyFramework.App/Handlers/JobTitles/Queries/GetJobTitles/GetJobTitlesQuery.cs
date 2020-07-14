using CrazyFramework.Dtos.JobTitles;
using CrazyFramework.App.Infrastructure.Repos;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.App.Handlers.JobTitles.Queries.GetJobTitles
{
	public class GetJobTitlesQuery : IRequest<JobTitleDto[]>
	{
		public class GetJobTitlesQueryHandler : IRequestHandler<GetJobTitlesQuery, JobTitleDto[]>
		{
			private readonly IJobTitleRepository _jobTitleRepository;

			public GetJobTitlesQueryHandler(IJobTitleRepository jobTitleRepository)
			{
				_jobTitleRepository = jobTitleRepository;
			}

			public async Task<JobTitleDto[]> Handle(GetJobTitlesQuery request, CancellationToken cancellationToken)
			{
				var jobTitles = await _jobTitleRepository.GetAll();

				return jobTitles.Select(p => new JobTitleDto
				{
					Id = p.Id,
					Name = p.Name,
					Description = p.Description
				}).ToArray();
			}
		}
	}
}