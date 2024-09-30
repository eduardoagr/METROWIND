namespace METROWIND.ViewModel {

    [QueryProperty(nameof(SelectedTurbine), "SelectedTurbine")]
    public partial class TurbineDetailPageViewModel : ObservableObject {

        const int HoursDay = 24;

        const int MinutesDay = 60;

        const double x = 0.0007;

        [ObservableProperty]
        Turbine? selectedTurbine;

        [ObservableProperty]
        double energyPerDay;

        [ObservableProperty]
        double energyPerHour;

        [ObservableProperty]
        double energyPerSeond;

        [ObservableProperty]
        double removedCo2PerSecond;

        [ObservableProperty]
        TimeSpan diff;

        [ObservableProperty]
        int daysPassedSinceInstallation;

        [ObservableProperty]
        double energyProduced;

        [ObservableProperty]
        double removedCo2Kilograms;

        [RelayCommand]
        void Appear() {

            if (SelectedTurbine == null) {

                return;

            }

            EnergyPerDay = RoundToDecimals(SelectedTurbine.TurbinePower * SelectedTurbine.TurbineCapacityFactor * HoursDay);

            EnergyPerHour = RoundToDecimals(EnergyPerDay / HoursDay, 4);

            EnergyPerSeond = RoundToDecimals(EnergyPerHour / MinutesDay, 5);

            RemovedCo2PerSecond = RoundToDecimals(EnergyPerSeond * SelectedTurbine.TutbineEmissionOffset, 5);

            DateTime today = DateTime.Today;

            Diff = today - SelectedTurbine.InstalationDateTime;

            DaysPassedSinceInstallation = Diff.Days;

            EnergyProduced = RoundToDecimals(EnergyPerDay * DaysPassedSinceInstallation);

            RemovedCo2Kilograms = RoundToDecimals(EnergyProduced * SelectedTurbine.TutbineEmissionOffset, 3);

        }

        public static double RoundToDecimals(double value, int decimals = 2) {
            return Math.Round(value, decimals);
        }


    }
}
