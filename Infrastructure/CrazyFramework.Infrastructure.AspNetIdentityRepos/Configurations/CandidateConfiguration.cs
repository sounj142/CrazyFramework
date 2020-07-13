using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal class CandidateConfiguration : IEntityTypeConfiguration<CandidateDAO>
	{
		public void Configure(EntityTypeBuilder<CandidateDAO> builder)
		{
			builder.BaseConfigure();
			builder.ToTable("Candidates");

			builder.Property(t => t.FirstName)
				.HasMaxLength(50)
				.IsRequired();
			builder.Property(t => t.MiddleName)
				.HasMaxLength(50);
			builder.Property(t => t.LastName)
				.HasMaxLength(50)
				.IsRequired();
			builder.Property(t => t.Email)
				.HasMaxLength(100)
				.IsRequired();
			builder.Property(t => t.Cc)
				.HasMaxLength(500);
			builder.Property(t => t.JobscoreUrl)
				.HasMaxLength(500);
			builder.Property(t => t.GitHubAccount)
				.HasMaxLength(100);
			builder.Property(t => t.Description)
				.HasMaxLength(1000);

			builder.HasOne(x => x.JobTitle)
				.WithMany(x => x.Candidates)
				.HasForeignKey(x => x.JobTitleId);
		}
	}
}