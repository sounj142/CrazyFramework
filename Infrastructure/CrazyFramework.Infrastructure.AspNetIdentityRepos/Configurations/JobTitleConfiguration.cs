using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal class JobTitleConfiguration : IEntityTypeConfiguration<JobTitleDAO>
	{
		public void Configure(EntityTypeBuilder<JobTitleDAO> builder)
		{
			builder.BaseConfigure();
			builder.ToTable("JobTitles");

			builder.Property(t => t.Name)
				.HasMaxLength(100)
				.IsRequired();

			builder.Property(t => t.Description)
				.HasMaxLength(1000);
		}
	}
}