using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace BinanceTradeBot.CoinMarketCap
{
    public class Client : IDisposable
    {
        private const string UrlBase = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/";
        private const string UrlPartList = "listings/latest";
        private const string UrlPartItem = "quotes/latest";
        private string apiKey;
        private HttpClient coinMarketCapclient;

        public Client(string apiKey)
        {
            this.apiKey = apiKey;
            this.coinMarketCapclient = new HttpClient();
        }

        List<string> GetConvertCurrencyList()
        {
            return new List<string> { "AUD", "BRL", "CAD", "CHF", "CNY", "EUR", "GBP", "HKD", "IDR", "INR", "JPY", "KRW", "MXN", "RUB" };
        }

        Currency GetCurrencyBySlug(string slug)
        {
            return this.CurrencyBySlugList(new List<string> { slug }, string.Empty).First();
        }

        Currency GetCurrencyBySymbol(string symbol)
        {
            return this.CurrencyBySymbolList(new List<string> { symbol }, string.Empty).First();
        }

        Currency GetCurrencyBySlug(string slug, string convertCurrency)
        {
            return this.CurrencyBySlugList(new List<string> { slug }, convertCurrency).First();
        }

        Currency GetCurrencyBySymbol(string symbol, string convertCurrency)
        {
            return this.CurrencyBySymbolList(new List<string> { symbol }, convertCurrency).First();
        }

        public IEnumerable<Currency> GetCurrencyBySlugList(string[] slugList)
        {
            return this.CurrencyBySlugList(slugList.ToList(), string.Empty);
        }

        public IEnumerable<Currency> GetCurrencyBySlugList(string[] slugList, string convertCurrency)
        {
            return this.CurrencyBySlugList(slugList.ToList(), convertCurrency);
        }

        public IEnumerable<Currency> GetCurrencyBySymbolList(string[] symbolList)
        {
            return this.CurrencyBySymbolList(symbolList.ToList(), string.Empty);
        }

        public IEnumerable<Currency> GetCurrencyBySymbolList(string[] symbolList, string convertCurrency)
        {
            return this.CurrencyBySymbolList(symbolList.ToList(), convertCurrency);
        }

        private IEnumerable<Currency> CurrencyBySlugList(List<string> slugList, string convertCurrency)
        {
            var queryArguments = new Dictionary<string, string>
            {
                {"slug", string.Join(",", slugList.Select(item => item.ToLower()))}
            };

            var client = this.GetWebApiClient(UrlPartItem, ref convertCurrency, queryArguments);
            var result = client.MakeRequest(Method.GET, convertCurrency, true);

            return result;
        }

        private IEnumerable<Currency> CurrencyBySymbolList(List<string> symbolList, string convertCurrency)
        {
            var queryArguments = new Dictionary<string, string>
            {
                {"symbol", string.Join(",", symbolList.Select(item => item.ToLower()))}
            };

            var client = this.GetWebApiClient(UrlPartItem, ref convertCurrency, queryArguments);
            var result = client.MakeRequest(Method.GET, convertCurrency, true);

            return result;
        }

        private void GetWebApiClient(string urlPart, ref string convertCurrency, Dictionary<string, string> queryArguments)
        {
            if (string.IsNullOrEmpty(convertCurrency))
            {
                convertCurrency = "USD";
            }

            queryArguments.Add("convert", convertCurrency);

            UriBuilder uri = new UriBuilder(UrlBase + urlPart);

            this.coinMarketCapclient.DefaultRequestHeaders.Accept.Clear();
            this.coinMarketCapclient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            this.coinMarketCapclient.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");
            
            this.coinMarketCapclient.BaseAddress = uri.Uri;

            this.coinMarketCapclient.BaseAddress.Query = string.Join("&", queryArguments.Select(item => item.Key + "=" + item.Value));

            var client = new WebApiClient(uri, queryArguments, this.apiKey);

            using var client = new HttpClient();
            var content = await client.GetStringAsync("https://api.openweathermap.org/data/2.5/weather?q=mexico&appid=your_api_key");
            Console.WriteLine(content);



            
            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "api.openweathermap.org";
            uriBuilder.Port = 443;
            uriBuilder.Path = "data/2.5/weather";
            uriBuilder.Query = "q=mexico&appid=your_api_key";

            Uri uri = uriBuilder.Uri;
            Console.WriteLine(uri);



            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:9000/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("api/products/1");
            if (response.IsSuccessStatusCode)
            {
                Product product = await response.Content.ReadAsAsync<Product>();
                Console.WriteLine("{0}\t${1}\t{2}", product.Name, product.Price, product.Category);
            }





            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri("https://api.openweathermap.org/data/2.5/weather");

            var response = await client.GetAsync("?q=mexico&appid=your_api_key");





            UriBuilder uriBuilder = new UriBuilder();
            uriBuilder.Scheme = "https";
            uriBuilder.Host = "api.openweathermap.org";
            uriBuilder.Path = "data/2.5/weather";
            uriBuilder.Query = "appid=your_api_key";

            HttpClient client = new HttpClient();
            client.BaseAddress = uriBuilder.Uri;

            var response = await client.GetAsync("?q=mexico");







            UriBuilder URL = new UriBuilder("https://sandbox-api.coinmarketcap.com/v1/cryptocurrency/listings/latest");

            NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);
            queryString["start"] = "1";
            queryString["limit"] = "5000";
            queryString["convert"] = "USD";

            URL.Query = queryString.ToString();

            var newClient = new WebClient();
            newClient.Headers.Add("X-CMC_PRO_API_KEY", API_KEY);
            newClient.Headers.Add("Accepts", "application/json");
            string returnValue = newClient.DownloadString(URL.ToString());





            var anotherClient = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, "https://sandbox-api.coinmarketcap.com/v2/cryptocurrency/quotes/historical?id=1765&count=10&interval=5m");
            request.Headers.Add("X-CMC_PRO_API_KEY", "b54bcf4d-1bca-4e8e-9a24-22ff2c3d462c");
            request.Headers.Add("Accept", "*/*");
            var anotherResponse = await anotherClient.SendAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await anotherResponse.Content.ReadAsStringAsync());
        }

        IEnumerable<Currency> GetCurrencies()
        {
            return this.Currencies(-1, string.Empty);
        }

        IEnumerable<Currency> GetCurrencies(string convertCurrency)
        {
            return this.Currencies(-1, convertCurrency);
        }

        IEnumerable<Currency> GetCurrencies(int limit)
        {
            return this.Currencies(limit, string.Empty);
        }

        IEnumerable<Currency> GetCurrencies(int limit, string convertCurrency)
        {
            return this.Currencies(limit, convertCurrency);
        }

        private List<Currency> Currencies(int limit, string convertCurrency)
        {
            var queryArguments = new Dictionary<string, string>
            {
                {"start", "1"}
            };

            if (limit > 0)
                queryArguments.Add("limit", limit.ToString());
            else
                queryArguments.Add("limit", "100");

            var client = this.GetWebApiClient(UrlPartList, ref convertCurrency, queryArguments);

            var result = client.MakeRequest(Method.GET, convertCurrency, false);
            return result;
        }
        
        private void ReleaseUnmanagedResources()
        {
            // TODO: release unmanaged resources here
            this.client.Dispose();
        }

        public void Dispose()
        {
            this.ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~Client()
        {
            this.ReleaseUnmanagedResources();
        }
    }
}
