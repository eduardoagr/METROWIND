namespace METROWIND.Services {
    public class TurbinesService(DeviceLanguageService deviceLanguageService) {

        public ObservableCollection<TurbinePin> GetTurbinePins(ICommand pinClickedCommand) {

            return
            [
                new TurbinePin
                {
                Turbine = new Turbine(deviceLanguageService)
                {
                    Id = 1,
                    Name = "Solar Generator",
                    Label = "Charge station",
                    Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                    Location = new Location(-2.151993, -79.886109),
                    InstalationDateTime = new DateTime(2024, 1, 1, 13, 00, 00),
                    Images =
                    [
                        "charge_station.png",
                        "wind_turbine.png"
                    ],
                    DataCollection =
                    [
                        new("Ipad", 10),
                        new("Iphone", 10),
                        new("MacBook", 10),
                        new("Mac", 10),
                        new("Others", 10)
                    ]
                },
                PinClickedCommand = pinClickedCommand
                }
            ];
        }
    }
}