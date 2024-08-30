using METROWIND.Models;

using System.Collections.ObjectModel;

namespace METROWIND.Services {

    public class TurbinesService {

        public static ObservableCollection<PinModel> GetPins() {

            return [
                   new() { 
                       Label = "Charge station",
                       Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                       Location = new Location( -2.151993, -79.886109) },
            ];

        }
    }
}
