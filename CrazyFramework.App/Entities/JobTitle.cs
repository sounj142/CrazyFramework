using System;

namespace CrazyFramework.App.Entities
{
	public class JobTitle
	{
		public Guid Id { get; private set; }
		public string Name { get; private set; }
		public string Description { get; private set; }

		public JobTitle(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}