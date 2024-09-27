namespace METROWIND.Models {

    public class GeoapifyResponse {

        [JsonPropertyName("results")]
        public List<GeoapifyResult> Results { get; set; } = [];
    }

    public class GeoapifyResult {

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("postcode")]
        public string? Postcode { get; set; }

        [JsonPropertyName("street")]
        public string? Street { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("formatted")]
        public string? Formatted { get; set; }
    }

}