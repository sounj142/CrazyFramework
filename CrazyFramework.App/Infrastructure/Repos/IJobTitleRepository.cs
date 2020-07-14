using CrazyFramework.App.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CrazyFramework.App.Infrastructure.Repos
{
	public interface IJobTitleRepository
	{
		Task<JobTitle> GetById(Guid id);

		Task<IList<JobTitle>> GetAll();

		Task Create(JobTitle jobTitle);

		Task Update(JobTitle jobTitle);

		Task Delete(Guid id);
	}
}