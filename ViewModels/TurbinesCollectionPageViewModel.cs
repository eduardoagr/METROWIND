using Map = Microsoft.Maui.ApplicationModel.Map;

namespace METROWIND.ViewModel
{
    public partial class TurbinesCollectionPageViewModel(
        ITurbineService turbineService,
        IAppService appService,
        IServiceProvider serviceProvider,
        ICommandHandler commandHandler,
        IConnectivity connectivity,
        NoInternetPopUp noInternetPopUp):
        AppShellViewModel(turbineService, appService, serviceProvider,
            commandHandler, connectivity, noInternetPopUp)
    {
        public CollectionView? TurbinesCollection;
        public SfComboBox? ColletionComboBox;

        [ObservableProperty]
        Turbine? turbine;

        [RelayCommand]
        async Task SelectedItemChange()
        {
            if (ColletionComboBox!.SelectedIndex == -1)
            {
                return;
            }

            var item = TurbinePins.ElementAt(
                ColletionComboBox.SelectedIndex);

            TurbinesCollection?.ScrollTo(ColletionComboBox.SelectedIndex,
                -1, ScrollToPosition.Center);

            var inputView = ColletionComboBox.Children[1] as Entry;

#if ANDROID || IOS
            if (KeyboardExtensions.IsSoftKeyboardShowing(inputView!))
            {
                await Task.Delay(200);
                await inputView!.HideKeyboardAsync(default);
            }
#else
            await Task.CompletedTask;
#endif
        }

        [RelayCommand]
        static async Task GotoLocation(TurbinePin turbinePin)
        {
            await Navigate(turbinePin);
        }

        public static async Task Navigate(TurbinePin turbinePin)
        {
            var location = new Location(turbinePin.Turbine!.Latitude, turbinePin.Turbine.Longitude);
            var options = new MapLaunchOptions
            {
                Name = turbinePin.Turbine.Name,
                NavigationMode = NavigationMode.Driving
            };

            try
            {
                await Map.OpenAsync(location, options);
            }
            catch (Exception)
            {
                // No map application available to open
            }
        }
    }
}

