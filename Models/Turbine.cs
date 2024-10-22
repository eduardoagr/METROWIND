namespace METROWIND.Models {

    public partial class Turbine : ObservableObject {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Label => Name;

        public string? Address { get; set; }

        public Location? Location { get; set; }

        public DateTime? InstalationDateTime { get; set; }

        public List<string>? Images { get; set; }

        public string? StringifyInstalationDate { get; set; }
    }
}