﻿// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.

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
				new ApiResource("CrazyWebApi", "Crazy API")
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
						"https://localhost:44348/signin-oidc",
					},
                    // where to redirect to after logout
					PostLogoutRedirectUris = {
						"https://localhost:44348/signout-callback-oidc"
					},

					AllowedScopes =
					{
						IdentityServerConstants.StandardScopes.OpenId,
						IdentityServerConstants.StandardScopes.Profile,
						IdentityServerConstants.StandardScopes.Email,
						"CrazyWebApi"
					}
				},

                //new Client
                //{
                //    ClientId = "Client.Implicit.UsePopup",
                //    ClientName = "Client.Implicit.UsePopup",

                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    RequireClientSecret = false,
                //    AllowAccessTokensViaBrowser = true,

                //    RedirectUris = {
                //        "http://localhost:5003/signin-popup-oidc",
                //        "http://localhost:5003/signout-popup-oidc",
                //    },
                //    PostLogoutRedirectUris = {
                //        "http://localhost:5003/",
                //        "http://localhost:5003/signout-popup-oidc"
                //    },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "api"
                //    }
                //},

                //new Client
                //{
                //    ClientId = "Client.Code.CustomizeUri",
                //    ClientName = "Client.Code.CustomizeUri",

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequireClientSecret = false,
                //    RequirePkce = true,

                //    RedirectUris = {
                //        "http://localhost:5006/fantastic-url-for-redirect",
                //        "http://localhost:5006/wonderful-link-for-popup-login",
                //        "http://localhost:5006/sign-out-popup-here",
                //    },
                //    PostLogoutRedirectUris = {
                //        "http://localhost:5006/",
                //        "http://localhost:5006/sign-out-popup-here"
                //    },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "api"
                //    }
                //},

                //new Client
                //{
                //    ClientId = "Client.Code.Complex",
                //    ClientName = "Client.Code.Complex",

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequireClientSecret = false,
                //    RequireConsent = false,
                //    RequirePkce = true,

                //    RedirectUris = {
                //        "http://localhost:5002/signin-popup-oidc",
                //        "http://localhost:5002/signin-callback-oidc",
                //        "http://localhost:5002/silent-callback-oidc",
                //    },
                //    PostLogoutRedirectUris = { "http://localhost:5002/" },

                //    AllowedScopes = new List<string>
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        IdentityServerConstants.StandardScopes.Address,
                //        "api_role",
                //        "api",
                //    },
                //    ////AllowOfflineAccess = true,
                //    AccessTokenLifetime = 80,
                //},

                //new Client
                //{
                //    ClientId = "Client.Implicit.RequiredLogin",
                //    ClientName = "Client.Implicit.RequiredLogin",

                //    AllowedGrantTypes = GrantTypes.Implicit,
                //    AllowAccessTokensViaBrowser = true,
                //    RequireClientSecret = false,

                //    RedirectUris = {
                //        "http://localhost:5004/signin-callback-oidc",
                //    },
                //    PostLogoutRedirectUris = { "http://localhost:5004/" },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "api"
                //    }
                //},

                //new Client
                //{
                //    ClientId = "Client.Code.RemotelyConfig",
                //    ClientName = "Client.Code.RemotelyConfig",

                //    AllowedGrantTypes = GrantTypes.Code,
                //    RequireClientSecret = false,
                //    RequirePkce = true,

                //    RedirectUris = {
                //        "http://localhost:5012/signin-callback-oidc",
                //    },
                //    PostLogoutRedirectUris = { "http://localhost:5012/" },

                //    AllowedScopes =
                //    {
                //        IdentityServerConstants.StandardScopes.OpenId,
                //        IdentityServerConstants.StandardScopes.Profile,
                //        IdentityServerConstants.StandardScopes.Email,
                //        "api"
                //    }
                //},
        };
	}
}