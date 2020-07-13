using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal class TestConfiguration : IEntityTypeConfiguration<TestDAO>
	{
		public void Configure(EntityTypeBuilder<TestDAO> builder)
		{
			builder.BaseConfigure();
			builder.ToTable("Tests");

			builder.Property(t => t.Name)
				.IsRequired()
				.HasMaxLength(200)
				.HasDefaultValue(string.Empty);

			builder.Property(t => t.Template)
				.IsRequired();
			builder.Property(t => t.Description)
				.HasMaxLength(1000);

			builder.HasOne(x => x.JobTitle)
				.WithMany(x => x.Tests)
				.HasForeignKey(x => x.JobTitleId);
		}
	}
}