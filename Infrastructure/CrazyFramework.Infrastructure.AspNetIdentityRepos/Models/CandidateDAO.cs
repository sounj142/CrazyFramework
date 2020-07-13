using System;
using System.Collections.Generic;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models
{
	internal class CandidateDAO : AuditableDAO
	{
		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
		public string Email { get; set; }
		public string Cc { get; set; }
		public string Description { get; set; }
		public Guid JobTitleId { get; set; }
		public string JobscoreUrl { get; set; }
		public string GitHubAccount { get; set; }
		public bool Deleted { get; set; }

		public JobTitleDAO JobTitle { get; set; }
		public List<ExamDAO> Exams { get; set; } = new List<ExamDAO>();
	}
}