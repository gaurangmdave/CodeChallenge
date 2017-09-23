using CodeChallenge.Helpers;
using CodeChallenge.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace CodeChallenge.ApiAdapter
{
    public sealed class WebApiAdapter
    {
        #region :: VARIBALES, PROPERTIES ::

        private HttpClient httpClient;
        private CancellationTokenSource cts;

        private Dictionary<string, string> searchQueryParams;

        private static WebApiAdapter webApiAdapter = null;

        #endregion

        #region :: SINGLETON INSTANCE ::

        public static WebApiAdapter WebApiAdapterInstance
        {
            get
            {
                if (webApiAdapter == null)
                {
                    webApiAdapter = new WebApiAdapter();
                }
                return webApiAdapter;
            }
        }

        #endregion

        #region :: CONSTRUCTOR ::

        public WebApiAdapter()
        {
            httpClient = new HttpClient();
            cts = new CancellationTokenSource();

            searchQueryParams = new Dictionary<string, string>();
        }

        #endregion

        #region :: FUNCTIONS ::

        public void SetGenderToSearchQuery(Constants.Genders Gender)
        {
            switch (Gender)
            {
                case Constants.Genders.Male:
                    searchQueryParams[Constants.APIKEY_GENDER] = "male";
                    break;
                case Constants.Genders.Female:
                    searchQueryParams[Constants.APIKEY_GENDER] = "female";
                    break;
            }
        }

        public void SetPageToSearchQuery(uint Page)
        {
            if (Page != 0)
            {
                searchQueryParams[Constants.APIKEY_PAGE] = Page.ToString();
            }
            else
            {
                searchQueryParams.Remove(Constants.APIKEY_PAGE);
            }
        }

        public void SetPageSizeToSearchQuery(uint Size)
        {
            if (Size != 0)
            {
                searchQueryParams[Constants.APIKEY_PAGESIZE] = Size.ToString();
            }
            else
            {
                searchQueryParams.Remove(Constants.APIKEY_PAGESIZE);
            }
        }

        public void SetFullTextToSearchQuery(string Text)
        {
            if (!string.IsNullOrWhiteSpace(Text))
            {
                searchQueryParams[Constants.APIKEY_FULLTEXT] = Text;
            }
            else
            {
                searchQueryParams.Remove(Constants.APIKEY_FULLTEXT);
            }
        }

        public async Task<IEnumerable<Facet>> GetFacetsAsync()
        {
            SetFullTextToSearchQuery(string.Empty);

            var resourceUri = generateQueryString("facets");

            try
            {
                var httpResponse = await httpClient.GetAsync(resourceUri).AsTask(cts.Token);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<IEnumerable<Facet>>(content);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return new List<Facet>();
        }

        public async Task<SearchResult<Article>> GetArticlesAsync(uint page = 1, uint size = 30)
        {
            SetPageToSearchQuery(page);

            SetPageSizeToSearchQuery(size);

            var resourceUri = generateQueryString("articles");

            try
            {
                var httpResponse = await httpClient.GetAsync(resourceUri).AsTask(cts.Token);

                if (httpResponse.IsSuccessStatusCode)
                {
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<SearchResult<Article>>(content);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

            return new SearchResult<Article>();
        }

        private Uri generateQueryString(string Entity)
        {
            var urlBuilder = new StringBuilder();
            urlBuilder.Append(Constants.BASEAPIURI).Append("/" + Entity + "?");

            Uri resourceUri;

            foreach (var item in searchQueryParams)
            {
                urlBuilder.Append(item.Key).Append("=").Append(item.Value).Append("&");
            }

            if (!Uri.TryCreate(urlBuilder.ToString(), UriKind.Absolute, out resourceUri))
            {
                throw new Exception("URI Creation Failed!");
            }

            return resourceUri;
        }

        #endregion 


    }


}
