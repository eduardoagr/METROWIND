using IMap = Microsoft.Maui.ApplicationModel.IMap;

namespace METROWIND.ViewModel
{

    public partial class TurbinesCollectionPageViewModel(HttpService service, DeviceLanguageService deviceLanguage,
        TurbinesService turbinesService, IMap map, IServiceProvider serviceProvider) :
        HomePageViewModel(service, deviceLanguage, turbinesService, serviceProvider)
    {

        CollectionView? TurbinesCollection;

        [ObservableProperty]
        Turbine? turbine;

        [RelayCommand]
        void PageEnter(CollectionView collectionView)
        {

            if (collectionView != null)
            {

                TurbinesCollection = collectionView;
            }
        }

        [RelayCommand]
        async Task SelectedItemChange(SfComboBox combo)
        {

            if (combo.SelectedIndex < 0)
            {
                return;
            }

            var item = Turbines.ElementAt(combo.SelectedIndex);
            TurbinesCollection?.ScrollTo(combo.SelectedIndex, -1, ScrollToPosition.Center);
            var inputView = combo.Children[1] as Entry;

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
        async Task GotoLocation(TurbinePin turbinePin)
        {
            await Navigate(turbinePin);
        }

        public async Task Navigate(TurbinePin turbinePin)
        {
            var location = new Location(turbinePin.Turbine!.Latitude, turbinePin.Turbine.Longitude);
            var options = new MapLaunchOptions
            {
                Name = turbinePin.Turbine.Name,
                NavigationMode = NavigationMode.Driving
            };

            try
            {
                await map.OpenAsync(location, options);
            }
            catch (Exception)
            {
                // No map application available to open
            }
        }
    }
}

