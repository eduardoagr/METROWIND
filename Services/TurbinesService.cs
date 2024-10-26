namespace METROWIND.Services {

    public class TurbinesService {
        int currentId = 0;

        private readonly DeviceLanguageService deviceLanguageService;
        public ObservableCollection<TurbinePin> TurbinePins { get; private set; } = [];

        public TurbinesService(DeviceLanguageService deviceLanguageService) {
            InitializeTurbinePins();

            this.deviceLanguageService = deviceLanguageService;
        }

        private void InitializeTurbinePins() {
            TurbinePins.Add(new TurbinePin {
                Turbine = new Turbine {
                    Username = currentId,
                    Name = "Estación Ciudadela Simón Bolívar",
                    Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                    Location = new Location(-2.151993, -79.886109),
                    InstalationDateTime = new DateTime(2024, 1, 1, 13, 00, 00),
                    StringifyInstalationDate = DateTime.Now.ToString("D"),
                    Images = ["charge_station.png", "wind_turbine.png"]
                },
                PinClickedCommand = null // Set this dynamically later
            });

            // New turbine
            TurbinePins.Add(new TurbinePin {
                Turbine = new Turbine {
                    Username = ++currentId, // Increment the ID for the new turbine
                    Name = "Estación La Libertad",
                    Address = "Calle 24 de Mayo, La Libertad 240204, Ecuador",
                    Location = new Location(-2.230234, -80.910807),
                    InstalationDateTime = new DateTime(2024, 3, 15, 10, 30, 00),
                    StringifyInstalationDate = DateTime.Now.ToString("D"),
                    Images = ["turbine_1.png", "wind_turbine_2.png"]
                },
                PinClickedCommand = null // Set this dynamically later
            });


        }

        public void AddTurbinePin(TurbinePin turbinePin, ICommand pinClickedCommand) {
            if (turbinePin != null) {
                turbinePin.Turbine!.Username = currentId++;
                turbinePin.PinClickedCommand = pinClickedCommand;
                TurbinePins.Add(turbinePin);
            }
        }

        public ObservableCollection<TurbinePin> GetTurbinePinsForUI(ICommand pinClickedCommand) {
            foreach (var pin in TurbinePins.OrderBy(t => t.Turbine?.InstalationDateTime)) {
                pin.PinClickedCommand = pinClickedCommand;
            }
            return TurbinePins;
        }
    }
}