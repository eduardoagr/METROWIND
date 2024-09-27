namespace METROWIND.Services {
    public class GeoapifyService(HttpService httpService) {

        public async Task<GeoapifyResponse> GetAutocompleteResunt(string query) {

            string url = $"https://api.geoapify.com/v1/geocode/autocomplete?text={query}&format=json&apiKey={AppConstants.GEOAPIFY_APIKEY}";

            var resut = await httpService.GetAsync<GeoapifyResponse>(url);

            return resut!;

        }
    }
}
