namespace METROWIND.ViewModel {

    public partial class StartupPageViewModel : ObservableObject {

        [RelayCommand]
        async Task Appearing() {

            // Wait for 2 seconds
            await Task.Delay(2500);

            // Then navigate to HomePage
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}", true);
        }
    }
}
