namespace METROWIND.Services
{
    public class HttpService: IHttpService
    {
        readonly HttpClient _httpClient;
        readonly ILogger<HttpService> _logger;


        public HttpService(HttpClient httpClient, ILogger<HttpService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;

            // Set User-Agent header once in the constructor
            _httpClient.DefaultRequestHeaders.Add(
                "User-Agent",
                "Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; Googlebot/2.1; +http://www.google.com/bot.html)");

        }

        public async Task<T?> GetAsync<T>(string url)
        {
            try
            {
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();

                var data = await response.Content.ReadFromJsonAsync<T>();
                return data;
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError("HttpRequestException in HttpService: {Message}", ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unexpected error in HttpService");
            }

            return default;
        }
    }
}