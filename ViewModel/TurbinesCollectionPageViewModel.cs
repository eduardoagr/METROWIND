namespace METROWIND.ViewModel {

    public partial class TurbinesCollectionPageViewModel(TurbinesService turbinesService,
        DeviceLanguageService languageService) :
        ChargingStationsMapPageViewModel(turbinesService) {

        CollectionView TurbinesCollection;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        string? turbineName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        string? turbineAddress = "Calle de Américo Castro, 28050 Madrid, Spain";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        DateTime? turbineInstalation;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        string? turbineFormattedDate;

        [ObservableProperty]
        bool isDatePickerOpen;

        SfDateTimePicker? DateTimePicker;

        public bool IsSaveEnable => !string.IsNullOrEmpty(TurbineName) &&
                                    !string.IsNullOrEmpty(TurbineAddress) &&
                                    !string.IsNullOrEmpty(TurbineFormattedDate);

        CultureInfo? currentCulture;

        [ObservableProperty]
        bool isDeleteButtonVisible;

        private readonly DeviceLanguageService deviceLanguageService = languageService;

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

        public ObservableCollection<GeoapifyResult> Suggestions { get; private set; } = [];

        [RelayCommand]
        async Task DeleteTurbine(object parameter) {

            if (parameter is Border border) {

                await border.TranslateTo(0, -border.Height, 400, Easing.CubicIn);// Move up by its height

                var turbine = (TurbinePin)border.BindingContext;

                // Remove the turbine from the collection
                Turbines.Remove(turbine);

                await Task.Delay(300);

                border.Scale = 1; // Ensure scale is reset
                border.TranslationY = 0;
            }

        }

        [RelayCommand]
        void ConfirmDate(DateTime dateTime) {

            if (DateTimePicker != null) {

                currentCulture = new CultureInfo(deviceLanguageService.GetDeviceLanguage());

                TurbineInstalation = dateTime;

                DateTimePicker.IsOpen = false;

                TurbineFormattedDate = TurbineInstalation?.ToString("D", currentCulture)!;

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

        [RelayCommand]
        async Task SaveAndClose(SfPopup popUp) {
            try {
                var random = new Random();

                for (int i = 0; i < 30; i++) {
                    // Generate random latitude and longitude for the turbine's location
                    var randomLatitude = random.NextDouble() * 180 - 90; // Latitude between -90 and 90
                    var randomLongitude = random.NextDouble() * 360 - 180; // Longitude between -180 and 180
                    var randomLocation = new Location(randomLatitude, randomLongitude); // Assuming you have a Location class for latitude and longitude

                    // You can also randomize turbine details or keep them constant
                    string randomTurbineName = $"Turbine {i + 1}";
                    string randomTurbineAddress = $"Address {i + 1}";

                    _turbinesService.AddTurbinePin(new TurbinePin {
                        Turbine = new Turbine(deviceLanguageService) {
                            Name = randomTurbineName,
                            Address = randomTurbineAddress,
                            Label = $"Turbine {i + 1}",
                            InstalationDateTime = DateTime.Now.AddDays(-i), // For variety in installation dates
                            Location = randomLocation
                        }
                    }, OnPinMarkerClickedCommand!);
                }
                popUp.IsOpen = false;
            }
            catch (Exception ex) {
                // Handle the exception (e.g., log it or show an error message to the user)
                Console.WriteLine($"Error adding turbines: {ex.Message}");
            }
        }


        //[RelayCommand]
        //async Task SaveAndClose(SfPopup popUp) {

        //    try {

        //        var turbineLocation = await GetLocation(TurbineAddress!);

        //        if (turbineLocation != null) {

        //            _turbinesService.AddTurbinePin(new TurbinePin {

        //                Turbine = new Turbine(deviceLanguageService) {
        //                    Name = TurbineName,
        //                    Address = TurbineAddress,
        //                    Label = "My new turbine",
        //                    InstalationDateTime = TurbineInstalation,
        //                    Location = turbineLocation
        //                },
        //            }, OnPinMarkerClickedCommand!);
        //        }
        //        popUp.IsOpen = false;
        //    }
        //    catch (Exception ex) {
        //        Handle the exception(e.g., log it or show an error message to the user)
        //        Console.WriteLine($"Error adding turbine: {ex.Message}");
        //    }
        //}

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
        void SelectedItemChange(int selectedIndex) {

            TurbinesCollection.ScrollTo(selectedIndex, -1, ScrollToPosition.Center);
        }
    }
}




