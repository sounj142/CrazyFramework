using CrazyFramework.Core.Domain;
using CrazyFramework.Core.Domain.Orders;
using CrazyFramework.Core.Domain.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Data
{
	public class ApplicationDbContext : DbContext
	{
		private readonly ICurrentUserService _currentUserService;

		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options,
			ICurrentUserService currentUserService,) : base(options)
		{ }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.UserId;
						entry.Entity.Created = _dateTime.Now;
						break;

					case EntityState.Modified:
						entry.Entity.LastModifiedBy = _currentUserService.UserId;
						entry.Entity.LastModified = _dateTime.Now;
						break;
				}
			}

			return base.SaveChangesAsync(cancellationToken);
		}

		public DbSet<Product> Products { get; set; }
		public DbSet<Order> Orders { get; set; }
		public DbSet<OrderItem> OrderItems { get; set; }
	}
}