﻿using CrazyFramework.Infrastructure.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CrazyFramework.Infrastructure.Repos.Configurations
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