namespace METROWIND.Models {

    public class Turbine(DeviceLanguageService deviceLanguageService) {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Label { get; set; }

        public string? Address { get; set; }

        public Location? Location { get; set; }

        public DateTime? InstalationDateTime { get; set; }

        public ObservableCollection<string>? Images { get; set; }

        public ObservableCollection<TurbineData>? DataCollection { get; set; }

        public string LocalizedInstalationDateTime {
            get {
                if (InstalationDateTime.HasValue) {

                    var currentCulture = new CultureInfo(deviceLanguageService.GetDeviceLanguage());
                    return InstalationDateTime.Value.ToString("D", currentCulture);
                }

                return string.Empty;
            }
        }

    }
}