using System;
using System.Threading.Tasks;
using CrazyFramework.App.Common;
using CrazyFramework.App.Entities.Accounts;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CrazyFramework.Infrastructure.AspNetIdentityRepos
{
	internal static class ApplicationDbContextSeed
	{
		public static async Task SeedAccountsAsync(UserManager<UserDAO> userManager, RoleManager<IdentityRole> roleManager)
		{
			if (!await roleManager.Roles.AnyAsync())
			{
				await roleManager.CreateAsync(new IdentityRole(RoleType.Administrator));
				await roleManager.CreateAsync(new IdentityRole(RoleType.User));
			}

			const string adminEmail = "hoang.luong@altsrc.net";
			const string adminPassword = "Hoang@123!";

			if (await userManager.Users.AllAsync(u => u.Email != adminEmail))
			{
				var defaultUser = new UserDAO { UserName = adminEmail, Email = adminEmail, EmailConfirmed = true };
				await userManager.CreateAsync(defaultUser, adminPassword);

				var createdUser = await userManager.FindByNameAsync(adminEmail);

				await userManager.AddToRolesAsync(createdUser, new[] { RoleType.Administrator, RoleType.User });
			}
		}

		public static async Task SeedJobTitlesAsync(ApplicationDbContext dbcontext)
		{
			var jobTileDbSet = dbcontext.Set<JobTitleDAO>();
			if (!await jobTileDbSet.AnyAsync())
			{
				jobTileDbSet.Add(
					new JobTitleDAO
					{
						Name = "Full Stack Developer",
						Description = "Full Stack Developer"
					});
				jobTileDbSet.Add(
					new JobTitleDAO
					{
						Name = "QC",
						Description = "QC"
					});
				await dbcontext.SaveChangesAsync();
			}
		}

		public static async Task SeedEvaluationSectionsAsync(ApplicationDbContext dbcontext)
		{
			var evaluationSectionsDbSet = dbcontext.Set<EvaluationSectionDAO>();
			if (!await evaluationSectionsDbSet.AnyAsync())
			{
				var sections = new[]
				{
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Accessibility",
						Multiplier = 1
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Functionality (Running the program)",
						Multiplier = 5,
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Workflow/Case Attention to Detail",
						Multiplier = 2
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Style/Appearance of Code",
						Multiplier = 1
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Error Handling",
						Multiplier = 2
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Understanding of C#",
						Multiplier = 3
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Design Patterns/Architecture",
						Multiplier = 3
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Best Practices",
						Multiplier = 3
					},
					new EvaluationSectionDAO
					{
						Id = Guid.NewGuid(),
						Name = "Bonus Points",
						Multiplier = 1
					}
				};

				evaluationSectionsDbSet.AddRange(sections);
				await dbcontext.SaveChangesAsync();
			}
		}
	}
}