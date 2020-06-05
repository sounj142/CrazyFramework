using CrazyFramework.Repos.Models.Orders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Repos.Configurations
{
	internal class OrderConfiguration : IEntityTypeConfiguration<OrderDAO>
	{
		public void Configure(EntityTypeBuilder<OrderDAO> builder)
		{
			builder.HasMany(x => x.Items)
				.WithOne(p => p.Order)
				.HasForeignKey(p => p.OrderId)
				.OnDelete(DeleteBehavior.Cascade);

			builder.Property(p => p.Amount)
				.HasColumnType(DefaultDbTypes.DecimalType);
		}
	}
}