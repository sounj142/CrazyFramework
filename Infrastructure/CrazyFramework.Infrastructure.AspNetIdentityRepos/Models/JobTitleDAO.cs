using System.Collections.Generic;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models
{
	internal class JobTitleDAO : AuditableDAO
	{
		public string Name { get; set; }
		public string Description { get; set; }
		public bool Deleted { get; set; }

		public List<TestDAO> Tests { get; set; } = new List<TestDAO>();
		public List<CandidateDAO> Candidates { get; set; } = new List<CandidateDAO>();
	}
}