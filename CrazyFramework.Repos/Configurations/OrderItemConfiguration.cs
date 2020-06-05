using CrazyFramework.Repos.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Repos.Configurations
{
	internal class OrderItemConfiguration : IEntityTypeConfiguration<OrderItemDAO>
	{
		public void Configure(EntityTypeBuilder<OrderItemDAO> builder)
		{
			builder.ToTable("OrderItems");

			builder.HasOne(x => x.Product)
				.WithMany()
				.HasForeignKey(x => x.ProductId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(p => p.Price)
				.HasColumnType(DefaultDbTypes.DecimalType);
		}
	}
}