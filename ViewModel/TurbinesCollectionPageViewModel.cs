namespace METROWIND.ViewModel {

    public partial class TurbinesCollectionPageViewModel(TurbinesService turbinesService,
        DeviceLanguageService languageService) :
        ChargingStationsMapPageViewModel(turbinesService) {

        CollectionView? TurbinesCollection;

        [ObservableProperty]
        public Turbine? turbine = new();


        [ObservableProperty]
        bool isDatePickerOpen;

        SfDateTimePicker? DateTimePicker;

        CultureInfo? currentCulture;

        private readonly DeviceLanguageService deviceLanguageService = languageService;

        public ObservableCollection<GeoapifyResult> Suggestions { get; private set; } = [];

        [RelayCommand]
        void PageEnter(CollectionView collectionView) {

            if (collectionView != null) {

                TurbinesCollection = collectionView;
            }
        }

        [RelayCommand]
        void OpenDatePicker(SfDateTimePicker views) {

            if (views != null) {

                DateTimePicker = views;

                views.IsOpen = true;
            }
        }

        [RelayCommand]
        void ConfirmDate(DateTime dateTime) {

            if (DateTimePicker != null) {

                currentCulture = new CultureInfo(deviceLanguageService.GetDeviceLanguage());

                Turbine!.InstalationDateTime = dateTime;

                DateTimePicker.IsOpen = false;

                Turbine.StringifyInstalationDate = Turbine.InstalationDateTime?.ToString("D", currentCulture)!;

            }
        }

        [RelayCommand]
        void CancelDate(SfDateTimePicker views) {

            if (views != null) {

                views.IsOpen = false;
            }
        }


        [RelayCommand]
        void AddNewTurbinePopUp(SfPopup popUp) {

            if (popUp != null) {

                popUp.IsOpen = true;
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
        async Task SaveAndClose(SfPopup popUp) {


            var turbineLocation = await GetLocation(Turbine!.Address!);

            if (turbineLocation != null) {

                _turbinesService.AddTurbinePin(new TurbinePin {

                    Turbine = new Turbine {
                        Name = Turbine.Name,
                        Address = Turbine.Address,
                        StringifyInstalationDate = Turbine.StringifyInstalationDate,
                        Location = turbineLocation
                    },
                }, OnPinMarkerClickedCommand!);

                popUp.IsOpen = false;

            }
        }

        async Task<Location?> GetLocation(string address) {

            try {
                IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);
                return locations?.FirstOrDefault();
            }
            catch (Exception ex) {
                // Handle the exception (e.g., log it or show an error message to the user)
                Console.WriteLine($"Error getting location: {ex.Message}");
                return null;
            }
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

