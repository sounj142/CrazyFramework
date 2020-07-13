using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal class EvaluationNoteConfiguration : IEntityTypeConfiguration<EvaluationNoteDAO>
	{
		public void Configure(EntityTypeBuilder<EvaluationNoteDAO> builder)
		{
			builder.BaseConfigure();
			builder.ToTable("EvaluationNotes");

			builder.Property(t => t.Value)
				.HasMaxLength(5000)
				.IsRequired();

			builder.Property(t => t.FieldType)
				.HasDefaultValue(EvaluationFieldType.Comment);

			builder.HasOne(t => t.Section)
				.WithMany(t => t.EvaluationNotes)
				.HasForeignKey(t => t.SectionId)
				.IsRequired();

			builder.HasOne(t => t.Exam)
				.WithMany(t => t.EvaluationNotes)
				.HasForeignKey(t => t.ExamId)
				.IsRequired();
		}
	}
}