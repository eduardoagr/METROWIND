namespace METROWIND.Services {

    public class TurbinesService {

        public static ObservableCollection<PinModel> GetPins(ICommand pinSelectedComman) {
            return
            [
                new PinModel{
                Label = "Charge station",
                Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                Location = new Location(-2.151993, -79.886109),
                MarkerClickedCommand = pinSelectedComman,
                Images = [
                    "charge_station.png",
                    "wind_turbine.png"
                ]
                }
            ];
        }
    }
}