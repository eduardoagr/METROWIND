namespace METROWIND.Services {
    public class TurbinesService {

        private const double IncrementValue = 0.0007;
        private readonly DeviceLanguageService _deviceLanguageService;
        private readonly IDispatcher _dispatcher; // Add a field for the dispatcher
        private ObservableCollection<TurbinePin> _turbinePins = [];
        private bool isTimerRunning = false;
        private IDispatcherTimer? _timer;

        public TurbinesService(DeviceLanguageService deviceLanguageService, IDispatcher application) {
            _deviceLanguageService = deviceLanguageService;
            _dispatcher = application;
            InitializeTimer(); // Just initialize the timer, do not start it here

        }

        // Initialize the timer
        private void InitializeTimer() {
            _timer = _dispatcher.CreateTimer(); // Use the injected dispatcher
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += (s, e) => UpdateTurbinesCo2();
        }

        public ObservableCollection<TurbinePin> GetTurbinePins(ICommand pinClickedCommand) {
            _turbinePins = [
                new() {
                    Turbine = new Turbine(_deviceLanguageService) {
                        Id = 1,
                        Name = "Estación Ciudadela Simón Bolívar",
                        Label = "Charge station",
                        Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                        Location = new Location(-2.151993, -79.886109),
                        InstalationDateTime = new DateTime(2024, 8, 2),
                        Images = ["charge_station.png", "wind_turbine.png"]
                    },
                    PinClickedCommand = pinClickedCommand
                }
            ];

            foreach (var pin in _turbinePins) {
                CalculateAndSetValues(pin.Turbine!);
            }

            // Do not start the timer here
            return _turbinePins;
        }

        // Start the timer explicitly
        public void StartTimer() {
            if (!isTimerRunning) {
                _timer!.Start();
                isTimerRunning = true;
            }
        }

        // Method to handle updating CO2 every second
        private void UpdateTurbinesCo2() {
            foreach (var pin in _turbinePins) {
                var turbine = pin.Turbine;
                if (turbine != null) {
                    turbine.RemovedCo2Kilograms += turbine.RemovedCo2PerSecond;  // Update CO2 every second
                    turbine.RemovedCo2Kilograms = RoundToDecimals(turbine.RemovedCo2Kilograms, 3);
                }
            }
        }

        private void CalculateAndSetValues(Turbine turbine) {
            var energyPerDay = RoundToDecimals(turbine.TurbinePower * turbine.TurbineCapacityFactor * 24);
            var energyPerHour = RoundToDecimals(energyPerDay / 24, 4);
            var energyPerSecond = RoundToDecimals(energyPerHour / 60, 5);
            var removedCo2PerSecond = RoundToDecimals(energyPerSecond * turbine.TurbineEmissionOffset, 5);
            var daysPassedSinceInstallation = (DateTime.Today - turbine.InstalationDateTime).Days;
            var energyProduced = RoundToDecimals(energyPerDay * daysPassedSinceInstallation);
            var removedCo2Kilograms = RoundToDecimals(turbine.CalculatedRemovedCo2Kilograms, 3);

            turbine.RemovedCo2Kilograms = removedCo2Kilograms;
        }

        private double RoundToDecimals(double value, int decimals = 2) {
            return Math.Round(value, decimals);
        }
    }
}
