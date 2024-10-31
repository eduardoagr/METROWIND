
namespace METROWIND.ViewModel {

    public partial class TurbinesCollectionPageViewModel(TurbinesService turbinesService,
        ILogger<TurbinesCollectionPageViewModel> logger)
        : ChargingStationsMapPageViewModel(turbinesService) {
        CollectionView? TurbinesCollection;

        public ObservableCollection<GeoapifyResult> Suggestions { get; private set; } = [];

        [RelayCommand]
        void PageEnter(CollectionView collectionView) {

            logger.LogInformation("Page loads correctly");

            if (collectionView != null) {

                TurbinesCollection = collectionView;
            }
        }

        [RelayCommand]
        void AddNewTurbinePopUp() {

            try {
                Shell.Current.GoToAsync($"{nameof(AddNewTurbinePage)}", true);
            }
            catch (Exception ex) {

                Debug.WriteLine($"Navigation error: {ex.Message}");
            }
        }

        [RelayCommand]
        async Task AutocompleteSuggestion(string val) {

            await GetSugestions(val);
        }

        [RelayCommand]
        void MouseEnter(Grid g) {
            if (g.Children[1] is Border border) {
                border.IsVisible = true;
            }
        }

        [RelayCommand]
        void MouseLeave(Grid g) {
            if (g.Children[1] is Border border) {
                border.IsVisible = false;
            }

        }


        async Task GetSugestions(string val) {

            //var results = await geoapifyService.GetAutocompleteResunt(val);

            Suggestions.Clear();
            //foreach (var item in results.Results) {

            //Suggestions.Add(item);

        }

        [RelayCommand]
        async Task DeleteTurbine(TurbinePin turbine) {

            await Task.Delay(300);
            Turbines.Remove(turbine);

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

