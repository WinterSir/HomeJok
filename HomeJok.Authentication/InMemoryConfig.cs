using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                new ApiResource("HomeJokApi", "获取用户信息API")
                {
                    Scopes={ "scope1" }//必须
                }
            };
        }

        /// <summary>
        /// ApiScopes 作用域
        /// </summary>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new ApiScope[]
              {
                new ApiScope("scope1")
              };
        }

        /// <summary>
        /// Client 客户端
        /// </summary>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    ClientId = "HomeJok.Authentication",                        //客户端唯一标识
                    ClientName = "Authentication",                              //客户端名称
                    ClientSecrets = new [] { new Secret("wintersir".Sha256()) },//客户端密码，进行了加密
                    AllowedGrantTypes = GrantTypes.ClientCredentials,           //授权方式，客户端认证 ClientId+ClientSecrets
                    AllowedScopes = new [] { "scope1" },                        //允许访问的资源
                    Claims = new List<ClientClaim>(){
                        new ClientClaim(IdentityModel.JwtClaimTypes.Role,"Admin"),
                        new ClientClaim(IdentityModel.JwtClaimTypes.NickName,"WinterSir"),
                        new ClientClaim("email","641187567@qq.com")
                    }
                }
            };
        }
    }
}
