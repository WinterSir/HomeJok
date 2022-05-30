using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace HomeJok.Authentication
{
    public class InMemoryConfig
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
        };
        /// <summary>
        /// ApiResource 资源列表
        /// </summary>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new[]
            {
                new ApiResource("HomeJokApi", "正式环境API")
                {
                    Scopes={ "HomeJokScope" }
                },
            };
        }
        /// <summary>
        /// ApiScopes 作用域
        /// </summary>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
            {
                new ApiScope("HomeJokScope"),
            };
        }
        /// <summary>
        /// Client 客户端
        /// </summary>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                //简化模式
                new Client
                {
                    ClientId = "antdview",
                    ClientName = "antdview",
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RedirectUris = { "https://homejok.wintersir.com/callback" },
                    PostLogoutRedirectUris = { "https://homejok.wintersir.com" },
                    RequireConsent = false, //禁用授权页面
                    AllowedCorsOrigins = new [] { "https://homejok.wintersir.com" ,"http://localhost:8000","http://localhost:4200"},
                    AllowedScopes = new []{
                        "HomeJokScope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    AllowAccessTokensViaBrowser = true  //允许将token通过浏览器传递
                },
                //授权码模式
                new Client
                {
                    ClientId = "antdviewcode",
                    ClientName = "antdviewcode",
                    ClientSecrets = new [] { new Secret("antdviewcode".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "https://homejok.wintersir.com/callback" },
                    PostLogoutRedirectUris = { "https://homejok.wintersir.com" },
                    RequireConsent = false, //禁用授权页面
                    AllowedCorsOrigins = new [] { "https://homejok.wintersir.com" ,"http://localhost:8000","http://localhost:4200"},
                    AllowedScopes = new []{
                        "HomeJokScope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    },
                    AllowAccessTokensViaBrowser = true  //允许将token通过浏览器传递
                },
                //混合模式
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "HomeJok Mvc Client",
                    ClientSecrets = new [] { new Secret("Hybrid".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = { "https://mvc.wintersir.com/signin-oidc" },
                    PostLogoutRedirectUris = { "https://mvc.wintersir.com/signout-callback-oidc" },
                    RequireConsent = false, //禁用授权页面
                    RequirePkce = false,    //混合模式需要修改对应服务端注册客户端时配置RequirePkce = false，这样不需要客户端提供code challeng
                    AllowedScopes = new []{
                        "HomeJokScope",
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                    }
                },
            };
        }
        public static List<TestUser> GetTestUser()
        {
            return new List<TestUser>(){
                new TestUser
                {
                    SubjectId = "1",
                    Username = "WinterSir",
                    Password = "WinterSir",
                    Claims =
                    {
                         new Claim(JwtClaimTypes.Name,"WinterSir"),
                         new Claim(JwtClaimTypes.GivenName,"WinterSir"),
                         new Claim(JwtClaimTypes.FamilyName,"WinterSir-FamilyName"),
                         new Claim(JwtClaimTypes.Email,"641187567@qq.com"),
                         new Claim(JwtClaimTypes.EmailVerified,"true", ClaimValueTypes.Boolean),
                         new Claim(JwtClaimTypes.WebSite,"http://WinterSir.com"),
                         new Claim(JwtClaimTypes.Address,
                            @" [ 'street_address': 'Chang Ping', 'locality': 'BeiJing' ,'postal_code’: 102206,'country': 'China'}",
                         IdentityServerConstants.ClaimValueTypes.Json)
                    }
                }
            };
        }
    }
}
