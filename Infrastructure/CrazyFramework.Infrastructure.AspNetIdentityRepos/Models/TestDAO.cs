using System;
using System.Collections.Generic;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Enums;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models
{
	internal class TestDAO : AuditableDAO
	{
		public Guid JobTitleId { get; set; }
		public TestDuration Duration { get; set; }
		public string Name { get; set; }
		public string Template { get; set; }
		public string Description { get; set; }
		public bool Deleted { get; set; }

		public JobTitleDAO JobTitle { get; set; }
		public List<ExamDAO> Exams { get; set; } = new List<ExamDAO>();
	}
}