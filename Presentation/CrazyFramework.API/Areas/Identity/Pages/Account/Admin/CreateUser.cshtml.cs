using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using CrazyFramework.App.Entities.Accounts;
using Microsoft.EntityFrameworkCore;
using CrazyFramework.App.BusinessServices;

namespace CrazyFramework.API.Areas.Identity.Pages.Account.Admin
{
	[Authorize(Roles = RoleType.Administrator)]
	public class CreateUserModel : PageModel
	{
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly UserManager<UserDAO> _userManager;
		private readonly ILogger<CreateUserModel> _logger;
		private readonly ICurrentRequestContext _requestContext;

		public CreateUserModel(
			UserManager<UserDAO> userManager,
			RoleManager<IdentityRole> roleManager,
			ILogger<CreateUserModel> logger,
			ICurrentRequestContext requestContext)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_logger = logger;
			_requestContext = requestContext;
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
			[Required]
			[EmailAddress]
			[Display(Name = "Email")]
			public string Email { get; set; }

			[Required]
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

		public async Task OnGetAsync()
		{
			var roleNames = await _roleManager.Roles
				.Select(role => role.Name)
				.OrderBy(role => role)
				.ToListAsync();

			Input = new InputModel
			{
				Roles = roleNames
					.Select(role => new RoleModel
					{
						Name = role,
						HasRole = role == RoleType.User
					})
					.ToList()
			};
		}

		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				var currentUserName = _requestContext.GetCurrentUserName();
				var user = new UserDAO { UserName = Input.Email, Email = Input.Email, EmailConfirmed = true };
				var result = await _userManager.CreateAsync(user, Input.Password);
				if (result.Succeeded)
				{
					var createdUser = await _userManager.FindByNameAsync(user.Email);

					var roleNames = Input.Roles
						.Where(role => role.HasRole)
						.Select(role => role.Name)
						.ToList();
					var assignRolesResult = await _userManager.AddToRolesAsync(createdUser, roleNames);

					if (assignRolesResult.Succeeded)
					{
						StatusMessage = $"Created user '{user.UserName}'.";
						_logger.LogInformation("{@User} created new account {@UserName} with role(s) {@Roles}.", currentUserName, user.UserName, string.Join(',', roleNames));
					}
					else
					{
						StatusMessage = $"Created user '{user.UserName}' but there was some problems when tried to assign user to role(s).";
						_logger.LogWarning("{@User} created new account {@UserName}, but failed when assigned him to role(s) {@Roles}.", currentUserName, user.UserName, string.Join(',', roleNames));
					}

					return RedirectToPage("Users");
				}
				foreach (var error in result.Errors)
				{
					ModelState.AddModelError(string.Empty, error.Description);
				}
			}

			// If we got this far, something failed, redisplay form
			return Page();
		}
	}
}