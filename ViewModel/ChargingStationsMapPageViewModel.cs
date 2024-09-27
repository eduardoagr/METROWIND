using Microsoft.Maui.Maps;

namespace METROWIND.ViewModel {

    public partial class ChargingStationsMapPageViewModel : ObservableObject {

        private readonly TurbinesService _turbinesService;

        private Microsoft.Maui.Controls.Maps.Map? MapView;

        public ObservableCollection<TurbinePin>? Turbines { get; set; }

        public ICommand? OnPinMarkerClickedCommand { get; }

        [ObservableProperty]
        bool isOptionsOpen;

        [ObservableProperty]
        bool isExpanded;

        public ChargingStationsMapPageViewModel(TurbinesService turbinesService) {

            OnPinMarkerClickedCommand = new Command<object>(OnPinMarkerClicked);

            _turbinesService = turbinesService;

            var SortedTurbinePins = _turbinesService.GetTurbinePins(OnPinMarkerClickedCommand)
                .OrderBy(t => t.Turbine!.InstalationDateTime);

            Turbines = new ObservableCollection<TurbinePin>(SortedTurbinePins);
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

            if (mapType == 0) {

                MapView!.MapType = MapType.Street;

            } else {

                MapView!.MapType = MapType.Satellite;
            }

            IsOptionsOpen = false;
        }
    }
}
