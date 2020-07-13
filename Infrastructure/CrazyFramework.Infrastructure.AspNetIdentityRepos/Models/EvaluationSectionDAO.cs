using System.Collections.Generic;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Models
{
	internal class EvaluationSectionDAO : AuditableDAO
	{
		public string Name { get; set; }
		public int Multiplier { get; set; }

		public List<EvaluationNoteDAO> EvaluationNotes { get; set; } = new List<EvaluationNoteDAO>();
	}
}