
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace METROWIND.ViewModel
{
    public partial class ChargingStationsMapPageViewModel: AppShellViewModel
    {
        public SfPopup? MapDialogPopUp;

        public Map? MapView;

        [ObservableProperty]
        bool isExpanded;

        public ChargingStationsMapPageViewModel(
            ITurbineService turbineService, IAppService
            appService, IServiceProvider serviceProvider,
            NoInternetPopUp noInternetPopUp,
            ICommandHandler commandHandler, IConnectivity connectivity)
            : base(turbineService, appService, serviceProvider, commandHandler, connectivity, noInternetPopUp)
        {
            MapDialogButtons();

        }


        public ObservableCollection<MapTypeButton> MapTypeButtons { get; set; } = [];

        private void MapDialogButtons()
        {
            MapTypeButtons.Add(new MapTypeButton
            {
                Caption = AppResource.Default,
                ImageName = MaterialFonts.Map,
                Selected = true,
                MapNumber = 1
            });
            MapTypeButtons.Add(new MapTypeButton
            {
                Caption = AppResource.Satelite,
                MapNumber = 2,
                ImageName = MaterialFonts.Satellite,
            });
        }

        [RelayCommand]
        void ItemSelected(Turbine Turbine)
        {
            if (Turbine == null || MapView == null)
            {
                return;
            }
            var mapSpan = MapSpan.FromCenterAndRadius(Turbine.Location,
                Distance.FromKilometers(2));

            MapView!.MoveToRegion(mapSpan);
        }

        [RelayCommand]

        void OpenMapLayerOptions()
        {
            MapDialogPopUp!.IsOpen = true;
        }

        [RelayCommand]
        void ChangeMapType(MapTypeButton mapTypeCustomButton)
        {
            if (mapTypeCustomButton == null)
            {
                return;
            }

            foreach (var mapTypeButton in MapTypeButtons)
            {
                mapTypeButton.Selected = false;
            }

            mapTypeCustomButton.Selected = true;

            switch (mapTypeCustomButton.MapNumber)
            {
                case 1:
                    MapView!.MapType = MapType.Street;
                    break;
                case 2:
                    MapView!.MapType = MapType.Satellite;
                    break;
            }

            MapDialogPopUp!.IsOpen = false;
        }
    }
}
