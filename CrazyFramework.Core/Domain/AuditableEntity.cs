using System;
using System.Collections.Generic;
using System.Text;

namespace CrazyFramework.Core.Domain
{
	public abstract class AuditableEntity : BaseEntity
	{
		public DateTime? CreatedDate { get; set; }
		public Guid? CreatedBy { get; set; }
		public DateTime LastModifyDate { get; set; }
		public Guid? LastModifiedBy { get; set; }
	}
}