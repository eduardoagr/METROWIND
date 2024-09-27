using METROWIND.Resources;

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
            } else {
                _shell!.FlyoutWidth = 300;
                
                InitializeFlyout();
            }

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

        public void InitializeFlyout() {

            var currentSelectedItem = _shell!.CurrentItem;

            var currentShellItems = _shell!.Items.ToList();

            _shell!.Items.Clear();

            foreach (var item in currentShellItems) {

                if (item.Title == AppResource.HomePage || item.Title == AppResource.Stations
                    || item.Title == AppResource.TurbinesCollection) {

                    _shell!.Items.Add(item);
                }
            }

            _shell.CurrentItem = currentSelectedItem;
        }

    }
}
