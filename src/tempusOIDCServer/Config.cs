using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace tempusOIDCServer
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApitResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("api1", "My API")
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                 new Client
                {
                   ClientId = "tempusAndroid",
                   RequireClientSecret = false,
                   ClientName = "Android app client",
                   // Authorisierungsprozess authentication_code
                   AllowedGrantTypes = GrantTypes.Code,
                   RequirePkce = true,
                   //AllowAccessTokensViaBrowser = true,
                   RequireConsent = false,

                   // RedirectURI für Android AppAuth
                   RedirectUris = { "at.c02.tempus:/oauth2redirect" },
                   AllowedScopes =
                   {
                           IdentityServerConstants.StandardScopes.OpenId,
                           IdentityServerConstants.StandardScopes.Profile, 
                           // Erlaubt Token-Refresh
                           IdentityServerConstants.StandardScopes.OfflineAccess,
                           // notwendig, um Zugriff auf API zu bekommen
                           "api1"
                   },
               },

                 new Client
                {
                    ClientId = "postmanTempus",
                    ClientName = "postmanTempus",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login??? ----> wohin soll er?
                    RedirectUris = {"https://www.getpostman.com/oauth2/callback"},

                    // where to redirect to after logout??? ----> wohin soll er?
                    PostLogoutRedirectUris = {"https://www.getpostman.com/oauth2/callback"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.Implicit,

                    // where to redirect to after login
                    RedirectUris = {"http://localhost:5002/signin-oidc"},

                    // where to redirect to after logout
                    PostLogoutRedirectUris = {"http://localhost:5002/signout-callback-oidc"},

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile
                    }
                },

                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = GrantTypes.ClientCredentials,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                },

                // resource owner password grant client
                new Client
                {
                    ClientId = "ro.client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {"api1"}
                }
            };
        }

        public static List<TestUser> GetUsers()
        {
            return new List<TestUser>
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "ckelley0",
                    Password = "password",
                    Claims = new List<Claim> {
                        new Claim(IdentityModel.JwtClaimTypes.Name, "ckelley0")
                    }
                },
                new TestUser()
                {
                    SubjectId = "2",
                    Username = "cbishop2",
                    Password = "password2",
                    Claims = new List<Claim> {
                        new Claim(IdentityModel.JwtClaimTypes.Name, "cbishop2")
                    }
                }
            };
        }
    }
}