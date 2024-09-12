namespace METROWIND.ViewModel {

    public partial class StartupPageViewModel : ObservableObject {

        [RelayCommand]
        void GotoHome() {
            Shell.Current.GoToAsync($"//{nameof(HomePage)}", true);
        }
    }
}
