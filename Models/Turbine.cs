namespace METROWIND.Models {

    public partial class Turbine(DeviceLanguageService deviceLanguageService) : ObservableObject {

        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Label { get; set; }

        public string? Address { get; set; }

        public Location? Location { get; set; }

        public DateTime InstalationDateTime { get; set; }

        public double TurbinePower { get; set; } = 0.37;

        public double TurbineCapacityFactor { get; set; } = 0.25;

        public double TurbineEmissionOffset { get; set; } = 0.45;

        public List<string>? Images { get; set; }

        [ObservableProperty]
        public double removedCo2Kilograms;

        #region Calulations

        public double EnergyPerDay => TurbinePower * TurbineCapacityFactor * 24;

        public double EnergyPerHour => EnergyPerDay / 24;

        public double EnergyPerSecond => EnergyPerHour / 60;

        public double RemovedCo2PerSecond => EnergyPerSecond * TurbineEmissionOffset;

        public int DaysPassedSinceInstallation => (DateTime.Today - InstalationDateTime).Days;

        public double EnergyProduced => EnergyPerDay * DaysPassedSinceInstallation;

        public double FinalRemovedCo2Kilograms => EnergyProduced * TurbineEmissionOffset;

        public double CalculatedRemovedCo2Kilograms => EnergyProduced * TurbineEmissionOffset;


        #endregion

        public string LocalizedInstalationDateTime {
            get {
                var currentCulture = new CultureInfo(deviceLanguageService.GetDeviceLanguage());
                return InstalationDateTime.ToString("D", currentCulture);
            }
        }

    }
}