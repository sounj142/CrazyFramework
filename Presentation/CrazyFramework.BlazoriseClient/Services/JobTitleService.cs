using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CrazyFramework.Dtos.JobTitles;

namespace CrazyFramework.BlazoriseClient.Services
{
	public class JobTitleService : ServiceBase, IJobTitleService
	{
		public JobTitleService(HttpClient httpClient) : base(httpClient)
		{
		}

		public Task<IList<JobTitleDto>> GetJobTitles()
		{
			return Get<IList<JobTitleDto>>("api/jobtitles");
		}

		public Task CreateJobTitle(CreateJobTitleDto jobTitle)
		{
			return Post("api/jobtitles", jobTitle);
		}

		public Task UpdateJobTitle(UpdateJobTitleDto jobTitle)
		{
			return Put($"api/jobtitles/{jobTitle.Id}", jobTitle);
		}

		public Task RemoveJobTitle(Guid jobTitleId)
		{
			return Delete($"api/jobtitles/{jobTitleId}");
		}
	}
}