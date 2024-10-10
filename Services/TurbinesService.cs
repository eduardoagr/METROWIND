using TurbineData = METROWIND.Models.TurbineData;

namespace METROWIND.Services {
    public class TurbinesService {

        public ObservableCollection<TurbinePin> _turbinePins = [];

        private readonly DeviceLanguageService _deviceLanguageService;

        public TurbinesService(DeviceLanguageService deviceLanguageService) {

            _deviceLanguageService = deviceLanguageService;

            // Initialize the collection with default turbine pins
            _turbinePins.Add(new TurbinePin {
                Turbine = new Turbine(deviceLanguageService) {
                    Id = 1,
                    Name = "My first turbine",
                    Label = "Charge station",
                    Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                    Location = new Location(-2.151993, -79.886109),
                    InstalationDateTime = new DateTime(2024, 1, 1, 13, 00, 00),
                    Images = [
                    "charge_station.png",
                    "wind_turbine.png"
                ],
                    DataCollection = [
                    new TurbineData("Ipad", 10),
                        new TurbineData("Iphone", 10),
                        new TurbineData("MacBook", 10),
                        new TurbineData("Mac", 10),
                        new TurbineData("Others", 10)
                ]
                },
                PinClickedCommand = null // Set this dynamically later
            });
        }

        public void AddTurbinePin(TurbinePin turbinePin) {
            if (turbinePin != null) {
                _turbinePins.Add(turbinePin);
            }
        }

        public ObservableCollection<TurbinePin> GetTurbinePinsForUI(ICommand pinClickedCommand) {
            var sortedPins = _turbinePins
                 .OrderBy(t => t.Turbine?.InstalationDateTime)
                 .ToList(); // Materialize the ordered collection

            foreach (var pin in sortedPins) {
                pin.PinClickedCommand = pinClickedCommand;
            }

            return new ObservableCollection<TurbinePin>(sortedPins);
        }
    }
}