using Syncfusion.Maui.Picker;
using Syncfusion.Maui.Popup;

namespace METROWIND.ViewModel {

    public partial class TurbinesCollectionPageViewModel(TurbinesService turbinesService, DeviceLanguageService deviceLanguageService) :
        ChargingStationsMapPageViewModel(turbinesService) {

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

                // Shrink animation
                await border.ScaleTo(1, 0, Easing.CubicOut); // 300ms for the animation duration


                var turbine = (TurbinePin)border.BindingContext;
                Turbines.Remove(turbine);

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
        void Cancel(SfDateTimePicker views) {

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

            var Turbinelocation = await GetLocation(TurbineAddress!);

            if (Turbinelocation != null) {

                turbinesService.AddTurbinePin(new TurbinePin {
                    Turbine = new Turbine(deviceLanguageService) {
                        Name = TurbineName,
                        Address = TurbineAddress,
                        Label = "My new trbine",
                        InstalationDateTime = TurbineInstalation,
                        Location = Turbinelocation
                    }

                });
            }

            popUp.IsOpen = false;
        }

        public async Task<Location?> GetLocation(string address) {

            IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);

            Location? location = locations?.FirstOrDefault();

            if (location != null) {

                return location;
            }

            return null;
        }
    }
}

