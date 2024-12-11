namespace METROWIND.Models
{
    [FirestoreData]
    public partial class Turbine: ObservableObject
    {
        [FirestoreProperty]
        public string? Id { get; set; }

        [FirestoreProperty]
        public string? Country { get; set; }

        [FirestoreProperty]
        public string? Name { get; set; }

        [FirestoreProperty]
        public string? Address { get; set; }

        [FirestoreProperty]
        public double Power { get; set; } = 0.37;

        [FirestoreProperty]
        public double Co2EmissionOffset { get; set; } = 0.45;

        [FirestoreProperty]
        public double CapacityFactor { get; set; } = 0.25;

        [FirestoreProperty]
        public double Latitude { get; set; }

        [FirestoreProperty]
        public double Longitude { get; set; }

        [FirestoreProperty]
        public DateTime InstalationDateTime { get; set; }

        [FirestoreProperty]
        public List<string>? ImagesURLs { get; set; } = [];

        public string? StringifyInstalationDate => InstalationDateTime.ToString("D", CultureInfo.CurrentCulture);

        public string? Label => Name; // Keeps the display label for the map

        public Location Location => new(Latitude, Longitude);

        [FirestoreProperty]
        public double EnergyPerDay => RoundToDecimals(Power * CapacityFactor * 24);

        [FirestoreProperty]
        public double EnergyPerHour => RoundToDecimals(EnergyPerDay / 24, 4);

        [FirestoreProperty]
        public double EnergyPerSecond => RoundToDecimals(EnergyPerHour / 60, 5);

        [FirestoreProperty]
        public double RemovedCo2PerSecond => RoundToDecimals(EnergyPerSecond * Co2EmissionOffset, 5);

        [FirestoreProperty]
        public double DaysPassedSinceInstallation => (DateTime.Today - InstalationDateTime).Days;

        [FirestoreProperty]
        public double EnergyProduced => RoundToDecimals(EnergyPerDay * DaysPassedSinceInstallation);

        [FirestoreProperty]
        public double RemovedCo2Kilograms { get; set; }

        [ObservableProperty]
        public double finalCo2Removed;


        private static double RoundToDecimals(double value, int decimals = 2)
        {
            return Math.Round(value, decimals);
        }

    }
}
