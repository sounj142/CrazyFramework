using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using System.Security.Claims;

namespace CrazyFramework.WebAPI.IntegrationTests
{
	public class TestAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
	{
		public TestAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options,
			ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock)
			: base(options, logger, encoder, clock)
		{
		}

		protected override Task<AuthenticateResult> HandleAuthenticateAsync()
		{
			if (Request.Headers.ContainsKey("Authorization") && Request.Headers["Authorization"].Contains(TestConstants.TestToken))
			{
				var claims = new[] {
					new Claim(ClaimTypes.Name, "Test user"),
					new Claim("sub", "id11102"),
					new Claim("email", "testuser@test.com")
				};
				var identity = new ClaimsIdentity(claims, "Test");
				var principal = new ClaimsPrincipal(identity);
				var ticket = new AuthenticationTicket(principal, "Test");

				var result = AuthenticateResult.Success(ticket);
				return Task.FromResult(result);
			}

			return Task.FromResult(AuthenticateResult.Fail("Missing Authorization Token"));
		}
	}
}