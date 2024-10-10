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

            }
        }

        [RelayCommand]
        async Task DeleteTurbine(object parameter) {

            if (parameter is Border border) {

                // Shrink animation
                await border.ScaleTo(1, 0, Easing.CubicOut); // 300ms for the animation duration


                var turbine = (TurbinePin)border.BindingContext;
                Turbines.Remove(turbine);

            }

        }



        [RelayCommand]
        async Task AddNewTurbinePopUp() {

            //await _popupService.ShowPopupAsync<AddTurbnePopUpViewModel>();
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

