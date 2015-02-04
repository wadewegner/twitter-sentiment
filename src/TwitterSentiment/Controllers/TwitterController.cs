using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TwitterOAuth.RestAPI.Models;
using TwitterOAuth.RestAPI.Resources;
using TwitterOAuth.RestAPI;
using TwitterSentiment.Models;

namespace TwitterSentiment.Controllers
{
    public class TwitterController : ApiController
    {
        private readonly string _apiKey = ConfigurationManager.AppSettings["ApiKey"];
        private readonly string _apiSecret = ConfigurationManager.AppSettings["ApiSecret"];
        private readonly string _accessToken = ConfigurationManager.AppSettings["AccessToken"];
        private readonly string _accessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"];
        private readonly string _alchemyApiKey = ConfigurationManager.AppSettings["AlchemyApiKey"];

        public async Task<SearchTweetsExtModel> Get([FromUri]string query)
        {
            query += " AND -RT";

            var secretModel = new SecretModel
            {
                ApiKey = _apiKey,
                ApiSecret = _apiSecret,
                AccessToken = _accessToken,
                AccessTokenSecret = _accessTokenSecret
            };

            var authorization = new Authorization(secretModel);
            var httpClient = new HttpClient();

            var uri =
                new Uri(string.Format("{0}?{1}", Urls.SearchTweets,
                    string.Format("q={0}&count=5", HttpUtility.UrlEncode(query))));

            var authHeader = authorization.GetHeader(uri);

            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("OAuth", authHeader);

            var request = new HttpRequestMessage
            {
                RequestUri = uri,
                Method = HttpMethod.Get
            };

            var responseMessage = await httpClient.SendAsync(request).ConfigureAwait(false);
            var response = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
            var jObject = JObject.Parse(response);
            var searchTweets = JsonConvert.DeserializeObject<SearchTweetsExtModel>(jObject.ToString());

            foreach (var status in searchTweets.statuses)
            {
                var alchemyUrl =
                    string.Format(
                        "http://access.alchemyapi.com/calls/text/TextGetTextSentiment?showSourceText=1&outputMode=json&apikey={0}&text={1}", status.text, _alchemyApiKey);


                var alchemyRequest = new HttpRequestMessage
                {
                    RequestUri = new Uri(alchemyUrl),
                    Method = HttpMethod.Get
                };

                var httpClient2 = new HttpClient();
                var alchemyResponseMessage = await httpClient2.SendAsync(alchemyRequest).ConfigureAwait(false);
                var alchemyResponse = await alchemyResponseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);

                var jAlchemyObject = JObject.Parse(alchemyResponse);
                var sentiment = JsonConvert.DeserializeObject<Sentiment>(jAlchemyObject.ToString());

                status.sentiment_score = Convert.ToDouble(sentiment.docSentiment.score);
                status.positive = (status.sentiment_score > 0);
            }

            return searchTweets;
        }
    }
}
