namespace METROWIND.Constants {
    public class AppConstants {

        public const string
            NEWS_URL = "https://newsapi.org/v2/everything?q=renewable+energy&apiKey=5963dd2ef5ff48b9bfa99e902bd55716";

        public static string GetNewsUrl(string languageCode) {

            return $"{NEWS_URL}&language={languageCode}";
        }

        public const string
            SYNCFUSION_KEY = "MzQ3NDQxM0AzMjM2MmUzMDJlMzBaM1BPVDR5cnhyS29NbGR2dm5ubVNNMlQ4aXdtVUREYUZOMW5HQktoVXdBPQ==";
    }
}
