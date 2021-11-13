﻿using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace MangoRestaurant.Services.Identity
{
    public static class SD
    {
        public const string Admin = "Admin";
        public const string Customer = "Customer";

        public static IEnumerable<IdentityResource> IdentityResources =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile()
            };

        public static IEnumerable<ApiScope> ApiScopes => 
            new List<ApiScope> 
            {
                new ApiScope(name: "mango", displayName: "Mango Server"),
                new ApiScope(name: "read", displayName: "read data"),
                new ApiScope(name: "write", displayName: "write data"),
                new ApiScope(name: "delete", displayName: "delete data")
            };

        public static IEnumerable<Client> Clients =>
            new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    AllowedScopes = { "read", "write", "profile" }
                },
                new Client
                {
                    ClientId = "mango",
                    ClientSecrets = { new Secret("secret".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new [] { "https://localhost:44378/signin-oidc" },
                    PostLogoutRedirectUris = { "https://localhost:44378/signout-callback-oidc" },
                    
                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "mango"
                    }
                },
            };
    }
}
