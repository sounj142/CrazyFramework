using System;

namespace CrazyFramework.Repos.Models
{
	public abstract class AuditableDAO : BaseDAO
	{
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime? LastModifyDate { get; set; }
		public Guid? LastModifiedBy { get; set; }
	}
}