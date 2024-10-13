using Microsoft.Maui.Maps;

namespace METROWIND.ViewModel {

    public partial class ChargingStationsMapPageViewModel : ObservableObject {

        protected readonly TurbinesService _turbinesService;

        private Microsoft.Maui.Controls.Maps.Map? MapView;

        public ICommand? OnPinMarkerClickedCommand { get; }

        [ObservableProperty]
        bool isOptionsOpen;

        [ObservableProperty]
        bool isExpanded;

        public ObservableCollection<TurbinePin> Turbines => _turbinesService._turbinePins;

        public ChargingStationsMapPageViewModel(TurbinesService turbinesService) {

            OnPinMarkerClickedCommand = new Command<object>(OnPinMarkerClicked);

            _turbinesService = turbinesService;

            _turbinesService.GetTurbinePinsForUI(OnPinMarkerClickedCommand);
        }

        [RelayCommand]
        private void Appearing(Microsoft.Maui.Controls.Maps.Map map) {

            if (map != null) {

                MapView = map;
            }
        }

        [RelayCommand]
        void ItemSelected(Turbine Turbine) {

            var mapSpan = MapSpan.FromCenterAndRadius(
                Turbine.Location!,
                Distance.FromKilometers(0.4));

            MapView!.MoveToRegion(mapSpan);
            IsExpanded = false;
        }

        void OnPinMarkerClicked(object turbine) {
            if (turbine != null) {
                // Handle the pin click event
                Shell.Current.GoToAsync($"{nameof(TurbineDetailPage)}",
                    true,
                    new Dictionary<string, object> {
                    { "SelectedTurbine", turbine }
                });
            };
        }

        [RelayCommand]
        void OpenMenu() {

            IsOptionsOpen = true;
        }

        [RelayCommand]
        void ChangeMapType(int mapType) {

            MapView!.MapType = mapType switch {
                0 => MapType.Street,
                1 => MapType.Satellite,
                _ => throw new ArgumentOutOfRangeException(nameof(mapType), mapType, null),
            };
            IsOptionsOpen = false;
        }
    }
}
