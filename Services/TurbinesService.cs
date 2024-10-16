namespace METROWIND.Services {

    public class TurbinesService {
        public ObservableCollection<TurbinePin> TurbinePins { get; private set; } = [];
        private readonly DeviceLanguageService _deviceLanguageService;

        public TurbinesService(DeviceLanguageService deviceLanguageService) {
            _deviceLanguageService = deviceLanguageService;
            InitializeTurbinePins();
        }

        private void InitializeTurbinePins() {
            TurbinePins.Add(new TurbinePin {
                Turbine = new Turbine(_deviceLanguageService) {
                    Id = 1,
                    Name = "My first turbine",
                    Label = "Charge station",
                    Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                    Location = new Location(-2.151993, -79.886109),
                    InstalationDateTime = new DateTime(2024, 1, 1, 13, 00, 00),
                    Images = ["charge_station.png", "wind_turbine.png"]
                },
                PinClickedCommand = null // Set this dynamically later
            });
        }

        public void AddTurbinePin(TurbinePin turbinePin, ICommand pinClickedCommand) {
            if (turbinePin != null) {
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