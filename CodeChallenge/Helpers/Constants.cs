namespace CodeChallenge.Helpers
{
    /// <summary>
    /// ALL THE CONSTANTS VARIABLES USED IN APPLICATION
    /// </summary>
    public class Constants
    {
        public const string BASEAPIURI = "https://api.zalando.com";

        public enum Genders
        {
            Male,
            Female
        }

        public const string APIKEY_GENDER = "gender";
        public const string APIKEY_PAGE = "page";
        public const string APIKEY_PAGESIZE = "pageSize";
        public const string APIKEY_FULLTEXT = "fullText";
    }
}
