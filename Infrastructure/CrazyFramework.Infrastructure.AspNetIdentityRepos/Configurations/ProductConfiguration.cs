using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos.Configurations
{
	internal class ProductConfiguration : IEntityTypeConfiguration<ProductDAO>
	{
		public void Configure(EntityTypeBuilder<ProductDAO> builder)
		{
			builder.ToTable("Products");

			builder.Property(t => t.Name)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(p => p.Price)
				.HasColumnType(DefaultDbTypes.DecimalType);
		}
	}
}