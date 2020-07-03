using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using CrazyFramework.App.Entities.Accounts;
using CrazyFramework.App.Helpers;
using Microsoft.EntityFrameworkCore;
using CrazyFramework.App.Entities;
using CrazyFramework.App.BusinessServices;

namespace CrazyFramework.API.Areas.Identity.Pages.Account.Admin
{
	[Authorize(Roles = RoleType.Administrator)]
	public class UsersModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<UserDAO> _userManager;
		private readonly AppSettings _appSettings;
		private readonly ICurrentRequestContext _requestContext;
		private readonly IIdentityLogic _identityLogic;

		public UsersModel(
			UserManager<UserDAO> userManager,
			RoleManager<IdentityRole> roleManager,
			AppSettings appSettings,
			ICurrentRequestContext requestContext,
			IIdentityLogic identityLogic)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_appSettings = appSettings;
			_requestContext = requestContext;
			_identityLogic = identityLogic;
		}

		[TempData]
		public string StatusMessage { get; set; }

		public class UserModel
		{
			public string UserName { get; set; }
			public bool AllowDelete { get; set; }

			public IList<string> Roles { get; set; }
		}

		public IList<UserModel> Users { get; set; }

		public async Task OnGetAsync()
		{
			var currentUser = _requestContext.GetCurrentUserName();
			var roleNames = await _roleManager.Roles
				.Select(role => role.Name)
				.OrderBy(role => role)
				.ToListAsync();

			Users = await _userManager.Users
				.Select(user => new UserModel
				{
					UserName = user.UserName,
					Roles = new List<string>()
				})
				.OrderBy(user => user.UserName)
				.ToListAsync();

			Users.ForEach(user => user.AllowDelete = _identityLogic.IsLegalToDeleteUser(user.UserName));

			foreach (var roleName in roleNames)
			{
				var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
				foreach (var user in usersInRole)
				{
					Users.FirstOrDefault(u => u.UserName == user.UserName)?.Roles.Add(roleName);
				}
			}
		}
	}
}