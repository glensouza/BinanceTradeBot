using System.Security.Claims;
using Microsoft.Identity.Client;

namespace BinanceTradeBot.MVC.Models
{
    public class GraphApiHelper
    {
        private string appId;
        private string redirectUri;
        private string appSecret;
        private string[] graphScopes;

        public GraphApiHelper()
        {
        }

        public async Task<string> GetUserAccessTokenAsync()
        {
            IConfidentialClientApplication? idClient = ConfidentialClientApplicationBuilder.Create(this.appId)
                .WithRedirectUri(this.redirectUri)
                .WithClientSecret(this.appSecret)
                .Build();

            //var tokenStore = new SessionTokenStore(idClient.UserTokenCache, HttpContext.Current, ClaimsPrincipal.Current);

            IEnumerable<IAccount>? accounts = await idClient.GetAccountsAsync();

            // By calling this here, the token can be refreshed
            // if it's expired right before the Graph call is made
            AuthenticationResult? result = await idClient
                .AcquireTokenSilent(this.graphScopes, accounts.FirstOrDefault())
                .ExecuteAsync();

            return result.AccessToken;
        }
    }
}
