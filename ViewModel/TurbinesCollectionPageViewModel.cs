
namespace METROWIND.ViewModel {

    public partial class TurbinesCollectionPageViewModel(HttpService service, DeviceLanguageService deviceLanguage,
        TurbinesService turbinesService) : HomePageViewModel(service, deviceLanguage, turbinesService) {

        CollectionView? TurbinesCollection;

        [ObservableProperty]
        Turbine? turbine;

        [RelayCommand]
        void PageEnter(CollectionView collectionView) {

            if (collectionView != null) {

                TurbinesCollection = collectionView;
            }
        }

        [RelayCommand]
        async Task SelectedItemChange(SfComboBox combo) {

            if (combo.SelectedIndex < 0) {
                return;
            }

            var item = Turbines.ElementAt(combo.SelectedIndex);
            TurbinesCollection?.ScrollTo(combo.SelectedIndex, -1, ScrollToPosition.Center);
            var inputView = combo.Children[1] as Entry;

#if ANDROID || IOS
            if (KeyboardExtensions.IsSoftKeyboardShowing(inputView!)) {
                await Task.Delay(200);
                await inputView!.HideKeyboardAsync(default);
            }
#else
            await Task.CompletedTask;
#endif
        }
    }
}

