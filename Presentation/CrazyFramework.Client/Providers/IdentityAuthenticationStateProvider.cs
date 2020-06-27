using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Authorization;

namespace CrazyFramework.Client.Providers
{
	public class IdentityAuthenticationStateProvider : AuthenticationStateProvider
	{
		// TODO1
		public override Task<AuthenticationState> GetAuthenticationStateAsync()
		{
			var identity = new ClaimsIdentity();
			return Task.FromResult(new AuthenticationState(new ClaimsPrincipal(identity)));
		}
	}
}