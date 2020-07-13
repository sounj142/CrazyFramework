using System;
using System.Collections.Generic;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Enums;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models
{
	internal class ExamDAO : AuditableDAO
	{
		public Guid CandidateId { get; set; }
		public Guid? TestId { get; set; }
		public ExamStatus Status { get; set; }
		public double? Score { get; set; }
		public DateTimeOffset? SentDate { get; set; }
		public DateTimeOffset? StartTime { get; set; }
		public string AccessCode { get; set; }
		public bool Deleted { get; set; }
		public TestDuration Duration { get; set; }

		public DateTimeOffset? EndTime { get; set; }
		public string GitHubRepositoryName { get; set; }
		public string GitHubInvitationId { get; set; }
		public string GitHubLink { get; set; }

		public CandidateDAO Candidate { get; set; }
		public TestDAO Test { get; set; }
		public List<EvaluationNoteDAO> EvaluationNotes { get; set; } = new List<EvaluationNoteDAO>();
	}
}