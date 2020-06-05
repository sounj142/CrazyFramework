using CrazyFramework.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Repos.Configurations
{
	internal class ProductConfiguration : IEntityTypeConfiguration<ProductDAO>
	{
		public void Configure(EntityTypeBuilder<ProductDAO> builder)
		{
			builder.Property(t => t.Name)
				.HasMaxLength(200)
				.IsRequired();

			builder.Property(p => p.Price)
				.HasColumnType(DefaultDbTypes.DecimalType);
		}
	}
}