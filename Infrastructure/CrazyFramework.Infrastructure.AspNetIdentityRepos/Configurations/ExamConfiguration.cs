using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal class ExamConfiguration : IEntityTypeConfiguration<ExamDAO>
	{
		public void Configure(EntityTypeBuilder<ExamDAO> builder)
		{
			builder.BaseConfigure();
			builder.ToTable("Exams");

			builder.Property(t => t.GitHubRepositoryName)
				.HasMaxLength(400);
			builder.Property(t => t.GitHubInvitationId)
				.HasMaxLength(100);
			builder.Property(t => t.GitHubLink)
				.HasMaxLength(400);
			builder.Property(t => t.AccessCode)
				.HasMaxLength(80);

			builder.HasOne(x => x.Candidate)
				.WithMany(x => x.Exams)
				.HasForeignKey(x => x.CandidateId);

			builder.HasOne(x => x.Test)
				.WithMany(x => x.Exams)
				.HasForeignKey(x => x.TestId);
		}
	}
}