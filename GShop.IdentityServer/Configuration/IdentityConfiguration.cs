﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace GShop.IdentityServer.Configuration;

public static class IdentityConfiguration
{
    public const string Admin = "Admin";
    public const string Client = "Client";

    public static IEnumerable<IdentityResource> IdentityResources =>
        new List<IdentityResource>
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

    public static IEnumerable<ApiScope> ApiScopes =>
        new List<ApiScope>
        {
            new ApiScope("gshop", "GShop Server"),
            new ApiScope(name:"read", "Read data."),
            new ApiScope(name:"write", "Write data."),
            new ApiScope(name:"delete", "Delete data."),
        };
    public static IEnumerable<Client> Clients =>
        new List<Client>
        {
            new Client
            {
                ClientId = "client",
                ClientSecrets = {new Secret("my_super_secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"read", "write", "profile"}

            },
            new Client
            {
                ClientId = "gshop",
                ClientSecrets = {new Secret("my_super_secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris = {"https://localhost:7180/signin-oidc"},
                PostLogoutRedirectUris = {"https://localhost:7180/signout-callback-oidc"},
                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "gshop"
                }

            }
        };
}
