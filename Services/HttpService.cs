namespace METROWIND.Services {

    public class HttpService(HttpClient httpClient, IConnectivity connectivity) {

        public async Task<T?> GetAsync<T>(string url) {

            if (connectivity.NetworkAccess != NetworkAccess.Internet) {

                await Shell.Current.DisplayAlert("Error",
                    "No internet connectivity"
                    , "OK");

                return default;

            }

            // Add User-Agent header
            httpClient.DefaultRequestHeaders.Add
                ("User-Agent",
                "Mozilla/5.0 AppleWebKit/537.36 (KHTML, like Gecko; compatible; Googlebot/2.1; +http://www.google.com/bot.html)");

            try {
                var response =
                    await httpClient.GetAsync(url);

                if (response.IsSuccessStatusCode) {
                    var jsonData = await response.Content.ReadAsStringAsync();

                    var data = await response.Content.ReadFromJsonAsync<T>();
                    return data;

                } else {

                    await Shell.Current.DisplayAlert("HTTP Error",
                        $"Status code: {response.StatusCode}", "OK");

                }

            } catch (HttpRequestException httpEx) {

                await Shell.Current.DisplayAlert("HTTP Error",
                    httpEx.Message, "OK");

            } catch (JsonException jsonEx) {

                await Shell.Current.DisplayAlert("JSON Error",
                    jsonEx.Message, "OK");

            } catch (Exception ex) {

                await Shell.Current.DisplayAlert("Unknown error",
                    ex.Message, "OK");
            }

            return default;

        }
    }
}