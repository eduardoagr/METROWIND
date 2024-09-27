namespace METROWIND.Constants {
    public class AppConstants {

        public const string
            NEWS_URL = "https://newsapi.org/v2/everything?q=renewable+energy&apiKey=5963dd2ef5ff48b9bfa99e902bd55716";

        public static string GetNewsUrl(string languageCode) {

            return $"{NEWS_URL}&language={languageCode}";
        }

        public const string
            SYNCFUSION_KEY = "MzQ4NTkxM0AzMjM3MmUzMDJlMzBsT1g5a3FQb1VvamorSzR3ZTVFVGpUcE5JYzh3SjJFazU4Ung5VjF2UXZnPQ==";

        //Address atcomplete 

        public const string GEOAPIFY_APIKEY = "d129ff64ae9a40c981d54d00d2d8a17e";
        public const string GEOAPIFY_API = "https://api.geoapify.com/v1/geocode/autocomplete";


    }
}
