using METROWIND.Resources;

namespace METROWIND.ViewModel {
    public partial class AppShellViewModel : ObservableObject {

        public const string FLYOUT_KEY = "flyouy_key";
        public const string SWITCH_KEY = "switch_key";
        public const string STATION_KEY = "station_key";

        private AppShell? _shell;

        [ObservableProperty]
        bool isCompactMode;

        [ObservableProperty]
        bool isMenuPopUpOen;

        [ObservableProperty]
        string? homeTitle;

        [ObservableProperty]
        string? stationsTitle;

        [RelayCommand]
        void Appearing(AppShell appShell) {
            _shell = appShell;

            HomeTitle = AppResource.HomePage;
            StationsTitle = AppResource.Stations;

        }

        [RelayCommand]
        void OpenMenu() {
            IsMenuPopUpOen = true;
        }

        [RelayCommand]
        void ToogleSwitch() {
            if (IsCompactMode) {
                _shell!.FlyoutWidth = 65;
            } else {
                _shell!.FlyoutWidth = 320;
            }

            IsMenuPopUpOen = false;

        }
    }
}
