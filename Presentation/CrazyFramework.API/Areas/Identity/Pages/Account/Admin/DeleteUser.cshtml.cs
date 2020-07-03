using System.Linq;
using System.Threading.Tasks;
using CrazyFramework.App.BusinessServices;
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
		private readonly ILogger<DeleteUserModel> _logger;
		private readonly IIdentityLogic _identityLogic;
		private readonly ICurrentRequestContext _requestContext;

		public DeleteUserModel(
			UserManager<UserDAO> userManager,
			ILogger<DeleteUserModel> logger,
			IIdentityLogic identityLogic,
			ICurrentRequestContext requestContext)
		{
			_userManager = userManager;
			_logger = logger;
			_identityLogic = identityLogic;
			_requestContext = requestContext;
		}

		[TempData]
		public string StatusMessage { get; set; }

		[BindProperty]
		public string UserName { get; set; }

		public async Task<IActionResult> OnGet(string userName)
		{
			UserName = userName;
			var user = await _userManager.FindByNameAsync(userName);
			if (user == null)
			{
				return NotFound($"Unable to load user '{UserName}'.");
			}

			return Page();
		}

		public async Task<IActionResult> OnPostAsync()
		{
			var currentUserName = _requestContext.GetCurrentUserName();

			var user = await _userManager.FindByNameAsync(UserName);
			if (user == null)
			{
				return NotFound($"Unable to load user '{UserName}'.");
			}

			if (!_identityLogic.IsLegalToDeleteUser(user.UserName))
			{
				ModelState.AddModelError(string.Empty, $"You do not have permission to delete user '{user.UserName}'.");
				return Page();
			}

			var result = await _userManager.DeleteAsync(user);
			if (!result.Succeeded)
			{
				ModelState.AddModelError(string.Empty, $"Unexpected error occurred when deleting user '{user.UserName}'.");

				_logger.LogWarning("Unexpected error occurred when {@User} deleting user {@UserName}, Errors: {@Errors}.", currentUserName, user.UserName,
					string.Join(", ", result.Errors.Select(x => x.Description)));
				return Page();
			}

			_logger.LogInformation("{@User} deleted user {@UserName}.", currentUserName, user.UserName);

			StatusMessage = $"Deleted user '{user.UserName}'.";

			return RedirectToPage("Users");
		}
	}
}