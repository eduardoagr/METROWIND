using Map = Microsoft.Maui.Controls.Maps.Map;

namespace METROWIND.ViewModel {

    public partial class ChargingStationsMapPageViewModel : ObservableObject {

        protected readonly TurbinesService _turbinesService;

        private Map? MapView;

        public ICommand? OnPinMarkerClickedCommand { get; }

        [ObservableProperty]
        bool isOptionsOpen;

        [ObservableProperty]
        bool isExpanded;

        public ObservableCollection<TurbinePin> Turbines => _turbinesService.TurbinePins;

        public ChargingStationsMapPageViewModel(TurbinesService turbinesService) {

            OnPinMarkerClickedCommand = new Command<object>(OnPinMarkerClicked);

            _turbinesService = turbinesService;

            _turbinesService.GetTurbinePinsForUI(OnPinMarkerClickedCommand);
        }

        [RelayCommand]
        private void Appearing(Map map) {

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
                2 => MapType.Hybrid, // Example: Handle mapType 2
                _ => throw new ArgumentOutOfRangeException(nameof(mapType), mapType, "Invalid map type"),
            };
            IsOptionsOpen = false;
        }
    }
}
