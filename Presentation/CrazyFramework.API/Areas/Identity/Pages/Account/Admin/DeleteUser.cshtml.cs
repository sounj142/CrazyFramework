using System.Threading.Tasks;
using CrazyFramework.App.Entities;
using CrazyFramework.Infrastructure.AspNetIdentityRepos.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CrazyFramework.API.Areas.Identity.Pages.Account.Admin
{
	public class DeleteUserModel : PageModel
	{
		private readonly UserManager<UserDAO> _userManager;
		private readonly SignInManager<UserDAO> _signInManager;
		private readonly ILogger<DeleteUserModel> _logger;
		private readonly AppSettings _appSettings;
		private readonly IIdentityLogic _identityLogic;

		public DeleteUserModel(
			UserManager<UserDAO> userManager,
			SignInManager<UserDAO> signInManager,
			ILogger<DeleteUserModel> logger,
			AppSettings appSettings,
			IIdentityLogic identityLogic)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_logger = logger;
			_appSettings = appSettings;
			_identityLogic = identityLogic;
		}

		[BindProperty]
		public InputModel Input { get; set; }

		public class InputModel
		{
			public string UserName { get; set; }
		}

		public async Task<IActionResult> OnGet(string userName)
		{
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
			{
				return NotFound($"Unable to load user '{userName}'.");
			}

			Input.UserName = userName;
			return Page();
		}

		//public async Task<IActionResult> OnPostAsync()
		//{
		//	var user = await _userManager.FindByNameAsync(Input.UserName);
		//	if (user == null)
		//	{
		//		return NotFound($"Unable to load user '{Input.UserName}'.");
		//	}

		//	if (_appSettings.SuperUsers.Any(userName => user.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)))
		//	{
		//		ModelState.AddModelError(string.Empty, $"User '{user.UserName}' is super user, you do not have permission to delete it.");
		//		return Page();
		//	}
		//	RequirePassword = await _userManager.HasPasswordAsync(user);
		//	if (RequirePassword)
		//	{
		//		if (!await _userManager.CheckPasswordAsync(user, Input.Password))
		//		{
		//		}
		//	}

		//	var result = await _userManager.DeleteAsync(user);
		//	var userId = await _userManager.GetUserIdAsync(user);
		//	if (!result.Succeeded)
		//	{
		//		throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
		//	}

		//	await _signInManager.SignOutAsync();

		//	_logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

		//	return Redirect("~/");
		//}
	}
}