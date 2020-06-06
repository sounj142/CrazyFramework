﻿using CrazyFramework.Core.Common;
using CrazyFramework.Core.Services;
using CrazyFramework.Repos.Models;
using CrazyFramework.Repos.Models.Orders;
using CrazyFramework.Repos.Models.Products;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Repos
{
	public class ApplicationDbContext : DbContext
	{
		private readonly ICurrentRequestContext _currentUserService;
		private readonly IDateTime _dateTimeService;

		public ApplicationDbContext(
			DbContextOptions<ApplicationDbContext> options,
			ICurrentRequestContext currentUserService,
			IDateTime dateTimeService) : base(options)
		{
			_currentUserService = currentUserService;
			_dateTimeService = dateTimeService;
		}

		public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
		{
			AutomaticSetupAuditData();
			return base.SaveChangesAsync(cancellationToken);
		}

		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			AutomaticSetupAuditData();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		public override int SaveChanges()
		{
			AutomaticSetupAuditData();
			return base.SaveChanges();
		}

		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			AutomaticSetupAuditData();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

			base.OnModelCreating(builder);
		}

		private void AutomaticSetupAuditData()
		{
			foreach (var entry in ChangeTracker.Entries<AuditableDAO>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.UserId;
						entry.Entity.CreatedDate = _dateTimeService.UtcNow;
						break;

					case EntityState.Modified:
						entry.Entity.LastModifiedBy = _currentUserService.UserId;
						entry.Entity.LastModifyDate = _dateTimeService.UtcNow;
						break;
				}
			}
		}
	}
}