using CommunityToolkit.Maui.Core;

namespace METROWIND.ViewModel {

    public partial class TurbinesCollectionPageViewModel(TurbinesService turbinesService, IPopupService popupService) : ChargingStationsMapPageViewModel(turbinesService) {

        CollectionView? TurbineCollectionView;

        private readonly IPopupService _popupService = popupService;

        [ObservableProperty]
        bool isDeleteButtonVisible;

        public ObservableCollection<GeoapifyResult> Suggestions { get; private set; } = [];

        [RelayCommand]
        void PageEnter(CollectionView collectionView) {

            if (collectionView != null) {

                TurbineCollectionView = collectionView;

                Console.WriteLine(Turbines.Count);
            }


        }

        [RelayCommand]
        async Task DeleteTurbine(object parameter) {

            if (parameter is Border border) {

                var animationTasks = new List<Task>
                {
                     border.ScaleYTo(0, 500, Easing.Linear),

                };

                // Wait for all animations to complete
                await Task.WhenAll(animationTasks);

                // Perform the deletion logic here
                if (border.BindingContext is TurbinePin turbine) {
                    Turbines.Remove(turbine);
                }

                // Optionally, you can add a delay to ensure the animation completes before layout updates
                await Task.Delay(300);

                // Reset the border properties
                border.ScaleY = 1;
            }

        }

        [RelayCommand]
        async Task AddNewTurbinePopUp() {

            await _popupService.ShowPopupAsync<AddTurbnePopUpViewModel>();
        }

        [RelayCommand]
        async Task AutocompleteSuggestion(string val) {

            await GetSugestions(val);
        }

        [RelayCommand]
        void MouseEnter() {

            IsDeleteButtonVisible = true;

        }

        [RelayCommand]
        void MouseLeave() {

            IsDeleteButtonVisible = false;

        }

        async Task GetSugestions(string val) {

            //var results = await geoapifyService.GetAutocompleteResunt(val);

            Suggestions.Clear();
            //foreach (var item in results.Results) {

            //Suggestions.Add(item);

        }
    }

}

