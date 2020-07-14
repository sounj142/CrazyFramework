using CrazyFramework.App.Entities;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Mapper
{
	internal static class JobTitleMapper
	{
		public static JobTitleDAO MapToDAO(this JobTitle jobTitle)
		{
			if (jobTitle == null)
				return null;

			var jobTitleDAO = new JobTitleDAO();
			jobTitle.MapToDAO(jobTitleDAO);
			return jobTitleDAO;
		}

		public static void MapToDAO(this JobTitle jobTitle, JobTitleDAO jobTitleDAO)
		{
			jobTitleDAO.Id = jobTitle.Id;
			jobTitleDAO.Name = jobTitle.Name;
			jobTitleDAO.Description = jobTitle.Description;
		}
	}
}