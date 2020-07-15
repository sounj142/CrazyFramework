using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrazyFramework.Dtos.JobTitles;

namespace CrazyFramework.BlazoriseClient.Services
{
	public interface IJobTitleService
	{
		Task<IList<JobTitleDto>> GetJobTitles();

		Task CreateJobTitle(CreateJobTitleDto jobTitle);

		Task UpdateJobTitle(UpdateJobTitleDto jobTitle);

		Task RemoveJobTitle(Guid jobTitleId);
	}
}