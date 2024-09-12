using Microsoft.Maui.Maps;

namespace METROWIND.ViewModel {

    public partial class ChargingStationsMapPageViewModel : ObservableObject {

        private Microsoft.Maui.Controls.Maps.Map? MapView;

        public ObservableCollection<PinModel>? Pins { get; set; }

        public ICommand? OnPinMarkerClickedCommand { get; }

        [ObservableProperty]
        bool isExpanded;

        public ChargingStationsMapPageViewModel() {

            OnPinMarkerClickedCommand = new Command<Pin>(OnPinMarkerClicked);

            Pins = TurbinesService.GetPins(OnPinMarkerClickedCommand);
        }

        [RelayCommand]
        private void Appearing(Microsoft.Maui.Controls.Maps.Map map) {

            if (map != null) {

                MapView = map;
            }
        }

        [RelayCommand]
        void ItemSelected(PinModel pinModel) {

            var mapSpan = MapSpan.FromCenterAndRadius(
                pinModel.Location!,
                Distance.FromKilometers(0.444));

            MapView!.MoveToRegion(mapSpan);
            IsExpanded = false;
        }

        void OnPinMarkerClicked(Pin pin) {
            if (pin != null) {
                // Handle the pin click event
                Shell.Current.GoToAsync($"{nameof(TurbineDetailPage)}",
                    true,
                    new Dictionary<string, object> {
                    { "SelectedPin", pin }
                });
            };
        }
    }
}
