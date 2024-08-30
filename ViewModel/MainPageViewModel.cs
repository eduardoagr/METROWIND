using METROWIND.Models;
using METROWIND.Services;

using Microsoft.Maui.Maps;

using System.Collections.ObjectModel;

namespace METROWIND.ViewModel {
    public partial class MainPageViewModel {

        public MapSpan? Span { get; set; }

        public ObservableCollection<PinModel> Pins { get; set; }

        public MainPageViewModel() {

            Pins = TurbinesService.GetPins();
            MoveMap();
        }

        void MoveMap() {
            var center = new Location(0.00000, 0.00000);
            var distance = Distance.FromMiles(10000000); // Adjust this value as needed
            Span = MapSpan.FromCenterAndRadius(center, distance);
        }
    }
}
