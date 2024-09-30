namespace METROWIND.Models {

    public class Turbine(DeviceLanguageService deviceLanguageService) {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Label { get; set; }

        public string? Address { get; set; }

        public Location? Location { get; set; }

        public DateTime InstalationDateTime { get; set; }

        public double TurbinePower { get; set; } = 0.37;

        public double TurbineCapacityFactor { get; set; } = 0.25;

        public double TutbineEmissionOffset { get; set; } = 0.45;

        public ObservableCollection<string>? Images { get; set; }

        public string LocalizedInstalationDateTime {
            get {
                    var currentCulture = new CultureInfo(deviceLanguageService.GetDeviceLanguage());
                    return InstalationDateTime.ToString("D", currentCulture);
            }
        }

    }
}