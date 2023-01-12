using BinanceTradeBot.MVC.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using Microsoft.Identity.Web;
using System.Diagnostics;
using System.Net.Http.Headers;
using BinanceTradeBot.Common;

namespace BinanceTradeBot.MVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly GraphProfileClient _graphProfileClient;
        private readonly ITokenAcquisition _tokenAcquisition;
        private readonly GraphServiceClient _graphClient;

        public string UserDisplayName { get; private set; } = string.Empty;
        public string UserPhoto { get; private set; }

        public HomeController(ILogger<HomeController> logger, GraphProfileClient graphProfileClient, ITokenAcquisition tokenAcquisition, GraphServiceClient graphClient)
        {
            this._logger = logger;
            this._graphProfileClient = graphProfileClient;
            this._graphClient = graphClient;
        }

        public async Task OnGetAsync()
        {
            // Acquire the access token.
            string[] scopes = new string[] { "user.read" };
            string accessToken = await this._tokenAcquisition.GetAccessTokenForUserAsync(scopes);
            this._logger.LogInformation($"Token: {accessToken}");

            // Use the access token to call a protected web API.
            HttpClient client = new();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            string json = await client.GetStringAsync("https://graph.microsoft.com/v1.0/me?$select=displayName");
            this._logger.LogInformation(json);

            User user = await this._graphProfileClient.GetUserProfile();
            this.UserDisplayName = user.DisplayName.Split(' ')[0];
            this.UserPhoto = await this._graphProfileClient.GetUserProfileImage();
        }
        
        public async Task<IActionResult> Index()
        {
            using StrategyMovingAverage strategy = new();
            await strategy.RunStrategyAsync();
            return this.View();
        }

        public IActionResult Privacy()
        {
            return this.View();
        }

        [AllowAnonymous]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}