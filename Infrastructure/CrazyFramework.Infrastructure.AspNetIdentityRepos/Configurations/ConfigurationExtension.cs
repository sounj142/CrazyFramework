using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal static class ConfigurationExtension
	{
		public static void BaseConfigure<T>(this EntityTypeBuilder<T> builder) where T : AuditableDAO
		{
			builder.Property(t => t.CreatedBy)
				.HasMaxLength(100);
			builder.Property(t => t.LastModifiedBy)
				.HasMaxLength(100);
		}
	}
}