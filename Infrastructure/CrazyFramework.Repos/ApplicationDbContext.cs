using CrazyFramework.App.BusinessServices;
using CrazyFramework.App.Common;
using CrazyFramework.Infrastructure.Repos.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace CrazyFramework.Infrastructure.Repos
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
			return base.SaveChangesAsync(cancellationToken);
		}

		// Note: we don't need to override SaveChangesAsync(CancellationToken cancellationToken = default) overload
		// because that method will call the method bellow, so we avoid calling AutomaticSetupAuditData method twice
		public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
		{
			AutomaticSetupAuditData();
			return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
		}

		// Note: we don't need to override SaveChanges() overload
		// because that method will call the method bellow, so we avoid calling AutomaticSetupAuditData method twice
		public override int SaveChanges(bool acceptAllChangesOnSuccess)
		{
			AutomaticSetupAuditData();
			return base.SaveChanges(acceptAllChangesOnSuccess);
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
		}

		private void AutomaticSetupAuditData()
		{
			foreach (var entry in ChangeTracker.Entries<AuditableDAO>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = _currentUserService.UserId;
						entry.Entity.CreatedDate = _dateTimeService.Now;
						break;

					case EntityState.Modified:
						entry.Entity.LastModifiedBy = _currentUserService.UserId;
						entry.Entity.LastModifyDate = _dateTimeService.Now;
						break;
				}
			}
		}
	}
}