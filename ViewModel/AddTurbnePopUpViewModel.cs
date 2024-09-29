namespace METROWIND.ViewModel {

    public partial class AddTurbnePopUpViewModel(DeviceLanguageService deviceLanguageService, TurbinesService turbinesService) : ObservableObject {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        string? turbineName;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        string? turbneAddress = "Calle de Américo Castro, 28050 Madrid, Spain";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        DateTime? turbineInstalation;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsSaveEnable))]
        string? turbineFormattedDate;

        [ObservableProperty]
        bool isDatePickerOpen;

        public bool IsSaveEnable => !string.IsNullOrEmpty(TurbineName) &&
                                    !string.IsNullOrEmpty(TurbneAddress) &&
                                    !string.IsNullOrEmpty(TurbineFormattedDate);

        SfDateTimePicker? DateTimePicker;

        CultureInfo? currentCulture;

        Location? TurbineLocation;

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

                TurbineInstalation = dateTime;

                DateTimePicker.IsOpen = false;

                TurbineFormattedDate = TurbineInstalation?.ToString("G", currentCulture)!;
            }
        }

        [RelayCommand]
        void Cancel(SfDateTimePicker views) {

            if (views != null) {

                views.IsOpen = false;
            }
        }

        [RelayCommand]
        async Task Save(Popup popup) {
            if (popup != null) {

                IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(TurbneAddress!);

                Location location = locations?.FirstOrDefault()!;

                if (location != null) {
                    TurbineLocation = location;
                }

                var newTurbine = new TurbinePin {
                    Turbine = new Turbine(deviceLanguageService) {
                        Id = 1,
                        Name = TurbineName,
                        Label = "Charge station",
                        Address = TurbneAddress,
                        Location = TurbineLocation,
                        InstalationDateTime = TurbineInstalation,
                        Images = null, DataCollection = null
                    }
                };

                turbinesService.AddTurbinePin(newTurbine);

                Console.WriteLine($"New Turbine added. Count: {turbinesService._turbinePins.Count}");

                await ExiitAnimation(popup);
            }
        }

        [RelayCommand]
        async Task Close(Popup popup) {

            await ExiitAnimation(popup);
        }

        private static async Task ExiitAnimation(Popup popup) {

            if (popup.Content is VisualElement popUpBorder) {

                // Set the anchor to the center
                popUpBorder.AnchorX = 0.5;
                popUpBorder.AnchorY = 0.5;

                await Task.WhenAll(new Task[]
                {
                popUpBorder.ScaleTo(0.5, 500, Easing.CubicInOut),
                popUpBorder.FadeTo(0, 500, Easing.CubicInOut)
                });

                popup.Close();
            }
        }
    }

}
