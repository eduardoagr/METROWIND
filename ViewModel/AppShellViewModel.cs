namespace METROWIND.ViewModel {
    public partial class AppShellViewModel : ObservableObject {

        private AppShell? _shell;

        [ObservableProperty]
        bool isCompactMode;

        [ObservableProperty]
        bool isMenuPopUpOen;

        [RelayCommand]
        void Appearing(AppShell appShell) {

            _shell = appShell;
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
            }

            IsMenuPopUpOen = false;
        }

    }
}
