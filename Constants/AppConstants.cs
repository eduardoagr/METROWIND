namespace METROWIND.Constants
{
    public class AppConstants
    {

        public const string
            NEWS_URL = "https://newsapi.org/v2/everything?q=renewable+energy&apiKey=5963dd2ef5ff48b9bfa99e902bd55716";

        public static string GetNewsUrl(string languageCode)
        {

            return $"{NEWS_URL}&language={languageCode}";
        }

        public const string
            SYNCFUSION_KEY = "MzU5ODkxOEAzMjM3MmUzMDJlMzBpNDFKeDlWQ0lqK3N0MWZ6eXkwNDFCc2JFU2RtMjAvKzQ2VnpGQjJ3SW1vPQ==";

        //Bing Maps

        public const string BINGMAPS_APIKEY = "2MJcwb3sDhOi7KnZYZFz~kBuOXKu5oDgZhJzhgiR6Tg~Akm14AZMJcKfXhu0JgJPCOuYTWsnRF3VWJ91UX0_nHRYa4zl082ffWsy7DV-id6a";

        //Firebae auth key

        public const string FIREBASEAUTHKEY = "AIzaSyCjgpfcOrD_mQSm1lwY7gwffY74pEDj3Zw";
        public const string FIREBASEDOMAIN = "metrowind-2e473.firebaseapp.com";

        public const string FIREBASE_PROJECT_ID = "metrowind-2e473";

        //Firebase Collection

        public const string COLLECTIONNAME = "turbines";

        //Azure connection string

        public const string AZURE_CONNECTION_STRING = "DefaultEndpointsProtocol=https;AccountName=metrowindstorage;AccountKey=ZmY3ys1l0YjKBm1cTRfo4CI6Xg8xuYfCQaitnmQcey1+nIQzZf3B7Jl27ubQcyrTMhbdSVeOYpwH+ASt6ShJNA==;EndpointSuffix=core.windows.net";

    }
}
