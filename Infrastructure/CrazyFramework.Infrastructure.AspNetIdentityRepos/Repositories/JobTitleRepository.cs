using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Mapper;
using System.Threading.Tasks;
using CrazyFramework.App.Common.Exceptions;
using Microsoft.Extensions.Logging;
using CrazyFramework.App.Infrastructure.Repos;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using CrazyFramework.App.Entities;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Repositories
{
	internal class JobTitleRepository : IJobTitleRepository
	{
		private readonly ApplicationDbContext _dbContext;
		private readonly ILogger<JobTitleRepository> _logger;
		private DbSet<JobTitleDAO> JobTitlesDbSet => _dbContext.Set<JobTitleDAO>();
		private IQueryable<JobTitleDAO> JobTitlesActualData => JobTitlesDbSet.Where(x => !x.Deleted);

		public JobTitleRepository(ApplicationDbContext dbContext, ILogger<JobTitleRepository> logger)
		{
			_dbContext = dbContext;
			_logger = logger;
		}

		public async Task<JobTitle> GetById(Guid id)
		{
			var jobTitle = await JobTitlesActualData.AsNoTracking()
				.Select(p => new
				{
					p.Id,
					p.Name,
					p.Description
				})
				.FirstOrDefaultAsync(p => p.Id == id);

			return jobTitle == null ? null : new JobTitle(id: jobTitle.Id, name: jobTitle.Name, description: jobTitle.Description);
		}

		public async Task<IList<JobTitle>> GetAll()
		{
			var jobTitles = (await JobTitlesActualData.AsNoTracking()
				.Select(p => new
				{
					p.Id,
					p.Name,
					p.Description
				})
				.OrderBy(p => p.Name)
				.ToListAsync())
				.Select(p => new JobTitle(id: p.Id, name: p.Name, description: p.Description))
				.ToList();

			return jobTitles;
		}

		public async Task Create(JobTitle jobTitle)
		{
			var jobTitleDAO = jobTitle.MapToDAO();
			JobTitlesDbSet.Add(jobTitleDAO);

			_logger.LogInformation("Creating job title {@Id}, {@Name}", jobTitle.Id, jobTitle.Name);

			await _dbContext.SaveChangesAsync();
		}

		public async Task Update(JobTitle jobTitle)
		{
			var jobTitleDAO = await JobTitlesActualData
				.FirstOrDefaultAsync(p => p.Id == jobTitle.Id);

			if (jobTitleDAO == null)
			{
				_logger.LogWarning("Update rejected. Job title {@JobTitleId} was not found.", jobTitle.Id);
				throw new NotFoundException("JobTitle", jobTitle.Id);
			}

			_logger.LogInformation("Updating job title {@Id}, {@Name}", jobTitle.Id, jobTitle.Name);

			jobTitle.MapToDAO(jobTitleDAO);

			await _dbContext.SaveChangesAsync();
		}

		public async Task Delete(Guid id)
		{
			var jobTitleDAO = await JobTitlesActualData
				.FirstOrDefaultAsync(p => p.Id == id);

			if (jobTitleDAO == null)
			{
				_logger.LogWarning("Deletion rejected. Job Title {@JobTitleId} was not found.", id);
				throw new NotFoundException("JobTitle", id);
			}

			_logger.LogInformation("Deleting job title {@Id}, {@Name}", jobTitleDAO.Id, jobTitleDAO.Name);

			jobTitleDAO.Deleted = true;
			await _dbContext.SaveChangesAsync();
		}
	}
}