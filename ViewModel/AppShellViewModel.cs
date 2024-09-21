namespace METROWIND.ViewModel {
    public partial class AppShellViewModel : ObservableObject {

        public const string FLYOUT_KEY = "flyouy_key";
        public const string SWITCH_KEY = "switch_key";

        private AppShell? _shell;

        [ObservableProperty]
        bool isCompactMode;

        [ObservableProperty]
        bool isMenuPopUpOen;

        [RelayCommand]
        void Appearing(AppShell appShell) {
            _shell = appShell;
            LoadConfigurations();
        }

        [RelayCommand]
        void OpenMenu() {
            IsMenuPopUpOen = true;
        }

        [RelayCommand]
        void ToogleSwitch() {
            if (IsCompactMode) {
                _shell!.FlyoutWidth = 65;
            } else { _shell!.FlyoutWidth = 320; }

            IsMenuPopUpOen = false;

            SaveConfigurations();
        }

        private void SaveConfigurations() {
            Preferences.Set(FLYOUT_KEY, _shell!.FlyoutWidth);
            Preferences.Set(SWITCH_KEY, IsCompactMode);
        }

        private void LoadConfigurations() {
            _shell!.FlyoutWidth = Preferences.Get(FLYOUT_KEY, 320.0);
            IsCompactMode = Preferences.Get(SWITCH_KEY, false);
        }
    }
}
