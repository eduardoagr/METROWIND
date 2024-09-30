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
                    InstalationDateTime = new DateTime(2024, 8, 2),
                    Images =
                    [
                        "charge_station.png",
                        "wind_turbine.png"
                    ],
                },
                PinClickedCommand = pinClickedCommand
                }
            ];
        }
    }
}