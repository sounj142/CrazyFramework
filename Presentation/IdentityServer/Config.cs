// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
	public static class Config
	{
		public static IEnumerable<IdentityResource> Ids =>
			new List<IdentityResource>
			{
				new IdentityResources.OpenId(),
				new IdentityResources.Profile(),
				new IdentityResources.Email()
			};

		public static IEnumerable<ApiResource> Apis =>
			new List<ApiResource>
			{
				new ApiResource("CrazyWebApi", "Crazy API", claimTypes: new [] {
					JwtClaimTypes.Name,
					JwtClaimTypes.Email,
					JwtClaimTypes.WebSite,
				})
			};

		public static IEnumerable<Client> Clients =>
			new List<Client>
			{
				new Client
				{
					ClientId = "SpaApp.Angular",
					ClientName = "SpaApp.Angular",
					ClientSecrets = { new Secret("secret".Sha256()) },

					AllowedGrantTypes = GrantTypes.Code,
					RequirePkce = true,

                     // where to redirect to after login
					RedirectUris = {
						"https://localhost:44310/signin-oidc",
					},
                    // where to redirect to after logout
					PostLogoutRedirectUris = {
						"https://localhost:44310/signout-callback-oidc"
					},
					// AccessTokenLifetime = 20,
					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"CrazyWebApi"
					}
				}
		};
	}
}