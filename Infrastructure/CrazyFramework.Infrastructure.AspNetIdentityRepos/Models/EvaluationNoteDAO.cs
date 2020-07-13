using System;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Enums;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models
{
	internal class EvaluationNoteDAO : AuditableDAO
	{
		public Guid ExamId { get; set; }
		public Guid SectionId { get; set; }
		public string Value { get; set; }
		public EvaluationFieldType FieldType { get; set; }

		public ExamDAO Exam { get; set; }
		public EvaluationSectionDAO Section { get; set; }
	}
}