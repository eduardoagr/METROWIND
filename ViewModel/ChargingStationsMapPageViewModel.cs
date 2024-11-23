using Map = Microsoft.Maui.Controls.Maps.Map;

namespace METROWIND.ViewModel
{
    public partial class ChargingStationsMapPageViewModel(
        HttpService service, DeviceLanguageService deviceLanguage, TurbinesService turbinesService,
        IServiceProvider serviceProvider) :
        HomePageViewModel(service, deviceLanguage, turbinesService, serviceProvider)
    {

        [ObservableProperty]
        Brush borderColor = Colors.Black;

        private Map? MapView;

        [ObservableProperty]
        bool isOptionsOpen;

        [ObservableProperty]
        bool isExpanded;

        [RelayCommand]
        private void Appearing(Map map)
        {
            MapView = map;
        }

        [RelayCommand]
        void ItemSelected(Turbine Turbine)
        {

            var mapSpan = MapSpan.FromCenterAndRadius(Turbine.Location,
                Distance.FromKilometers(0.4));

            MapView!.MoveToRegion(mapSpan);
            IsExpanded = false;
        }

        [RelayCommand]
        void OpenMenu()
        {
            IsOptionsOpen = true;
        }

        [RelayCommand]
        void ChangeMapType(int mapType)
        {
            MapView!.MapType = mapType switch
            {
                0 => MapType.Street,
                1 => MapType.Satellite,
                2 => MapType.Hybrid, // Example: Handle mapType 2
                _ => throw new ArgumentOutOfRangeException(nameof(mapType), mapType, "Invalid map type"),
            };
            IsOptionsOpen = false;
        }
    }
}
