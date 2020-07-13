using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal class EvaluationSectionConfiguration : IEntityTypeConfiguration<EvaluationSectionDAO>
	{
		public void Configure(EntityTypeBuilder<EvaluationSectionDAO> builder)
		{
			builder.BaseConfigure();
			builder.ToTable("EvaluationSections");

			builder.Property(t => t.Name)
				.HasMaxLength(150)
				.IsRequired();

			builder.Property(t => t.Multiplier)
				.HasDefaultValue(1);
		}
	}
}