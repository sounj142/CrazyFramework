using System;

namespace CrazyFramework.Dtos.JobTitles
{
	public class UpdateJobTitleDto
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string Description { get; set; }
	}
}