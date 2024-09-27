using CommunityToolkit.Maui.Core;

namespace METROWIND.ViewModel {

    public partial class TurbinesCollectionPageViewModel(TurbinesService turbinesService, IPopupService popupService)
        : ChargingStationsMapPageViewModel(turbinesService) {

        [ObservableProperty]
        bool isPopupOpen;

        public ObservableCollection<GeoapifyResult> Suggestions { get; private set; } = [];

        [RelayCommand]
        void DeleteTurbine(object o) {

            if (o != null && o is TurbinePin turbinePin) {
                Turbines!.Remove(turbinePin);
            }
        }

        [RelayCommand]
       async Task AddNewTurbinePopUp() {

            await popupService.ShowPopupAsync<AddTurbnePopUpViewModel>();
        }

        [RelayCommand]
        async Task AutocompleteSuggestion(string val) {

            await GetSugestions(val);
        }

        async Task GetSugestions(string val) {

            //var results = await geoapifyService.GetAutocompleteResunt(val);

            Suggestions.Clear();
            //foreach (var item in results.Results) {

            //Suggestions.Add(item);

        }
    }

}

