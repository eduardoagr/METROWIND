namespace METROWIND.Models {
    public class TrbineData(string product, double salesRate) {

        public string Product { get; set; } = product;

        public double SalesRate { get; set; } = salesRate;
    }
}
