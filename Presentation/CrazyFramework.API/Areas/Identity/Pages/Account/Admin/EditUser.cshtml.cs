using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.App.BusinessServices;
using CrazyFramework.App.Entities.Accounts;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CrazyFramework.API.Areas.Identity.Pages.Account.Admin
{
	[Authorize(Roles = RoleType.Administrator)]
	public class EditUserModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<UserDAO> _userManager;
		private readonly ILogger<EditUserModel> _logger;
		private readonly ICurrentRequestContext _requestContext;
		private readonly IIdentityLogic _identityLogic;

		public EditUserModel(
			UserManager<UserDAO> userManager,
			RoleManager<IdentityRole> roleManager,
			ILogger<EditUserModel> logger,
			ICurrentRequestContext requestContext,
			IIdentityLogic identityLogic)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
			_requestContext = requestContext;
			_identityLogic = identityLogic;
		}

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public InputModel Input { get; set; }

		public class RoleModel
		{
			public string Name { get; set; }
			public bool HasRole { get; set; }
		}

		public class InputModel
		{
			public string UserName { get; set; }

			[StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
			[DataType(DataType.Password)]
			[Display(Name = "Password")]
			public string Password { get; set; }

			[DataType(DataType.Password)]
			[Display(Name = "Confirm password")]
			[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
			public string ConfirmPassword { get; set; }

			public IList<RoleModel> Roles { get; set; }
		}

		public async Task<IActionResult> OnGetAsync(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
			{
				return NotFound($"Unable to load user '{userName}'.");
			}

			var allRoles = await _roleManager.Roles
				.Select(role => role.Name)
				.OrderBy(role => role)
				.ToListAsync();

			var userRoles = await _userManager.GetRolesAsync(user);

			Input = new InputModel
			{
				UserName = userName,
				Roles = allRoles
					.Select(role => new RoleModel
					{
						Name = role,
						HasRole = userRoles.Contains(role)
					})
					.ToList()
			};

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var user = await _userManager.FindByNameAsync(Input.UserName);
			if (user == null)
			{
				return NotFound($"Unable to load user '{Input.UserName}'.");
			}

			if (ModelState.IsValid)
			{
				if (!await UpdateRoleAsync(user))
				{
					return Page();
				}

				if (!await ChangePasswordIfNeededAsync(user))
				{
					return Page();
				}

				StatusMessage = $"Updated user '{user.UserName}'.";
				return RedirectToPage("Users");
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}

		private async Task<bool> UpdateRoleAsync(UserDAO user)
		{
			var currentUserName = _requestContext.GetCurrentUserName();

			var newRoles = Input.Roles
				.Where(role => role.HasRole)
				.Select(role => role.Name)
				.ToList();
			var currentRoles = await _userManager.GetRolesAsync(user);

			var rolesToRemove = currentRoles.Where(r => !newRoles.Contains(r)).ToList();
			var rolesToAdd = newRoles.Where(r => !currentRoles.Contains(r)).ToList();

			if (rolesToRemove.Count > 0)
			{
				// don't allow remove super users from Admin role
				if (_identityLogic.IsSuperAdminUser(user.UserName) && rolesToRemove.Contains(RoleType.Administrator))
				{
					ModelState.AddModelError(string.Empty, $"User {user.UserName} is super admin, you don't have permission to remove it from {RoleType.Administrator} role.");
					return false;
				}

				var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, rolesToRemove);
				if (!removeRolesResult.Succeeded)
				{
					var errors = AddToModelStateErrors(removeRolesResult);
					_logger.LogInformation("{@User} failed when removed {@UserName} from role(s) {@Roles}, Errors {@Errors}.", currentUserName, user.UserName,
						string.Join(',', rolesToRemove), errors);
					return false;
				}
			}

			if (rolesToAdd.Count > 0)
			{
				var addRolesResult = await _userManager.AddToRolesAsync(user, rolesToAdd);
				if (!addRolesResult.Succeeded)
				{
					var errors = AddToModelStateErrors(addRolesResult);
					_logger.LogInformation("{@User} failed when added {@UserName} to role(s) {@Roles}, Errors {@Errors}.", currentUserName, user.UserName,
						string.Join(',', rolesToAdd), errors);
					return false;
				}
			}

			return true;
		}

		private async Task<bool> ChangePasswordIfNeededAsync(UserDAO user)
		{
			if (string.IsNullOrEmpty(Input.Password))
				return true;

			var currentUserName = _requestContext.GetCurrentUserName();
			var code = await _userManager.GeneratePasswordResetTokenAsync(user);

			var result = await _userManager.ResetPasswordAsync(user, code, Input.Password);
			if (!result.Succeeded)
			{
				var errors = AddToModelStateErrors(result);
				_logger.LogInformation("{@User} failed when change {@UserName} password, Errors {@Errors}.", currentUserName, user.UserName, errors);
				return false;
			}

			return true;
		}

		private string AddToModelStateErrors(IdentityResult result)
		{
			foreach (var error in result.Errors)
			{
				ModelState.AddModelError(string.Empty, error.Description);
			}
			return string.Join(',', result.Errors.Select(x => x.Description));
		}
	}
}