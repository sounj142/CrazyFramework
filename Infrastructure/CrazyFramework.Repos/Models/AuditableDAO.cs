using System;

namespace CrazyFramework.Infrastructure.Repos.Models
{
	public abstract class AuditableDAO : BaseDAO
	{
		public DateTimeOffset? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTimeOffset? LastModifyDate { get; set; }
		public Guid? LastModifiedBy { get; set; }
	}
}