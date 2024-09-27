namespace METROWIND.ViewModel {
    public partial class AddTurbnePopUpViewModel(DeviceLanguageService deviceLanguageService) : ObservableObject {

        [ObservableProperty]
        string? turbinaName;

        [ObservableProperty]
        string? turbneAddres;

        [ObservableProperty]
        DateTime? turbneDate;

        [ObservableProperty]
        string turbineFormatedDate;

        [ObservableProperty]
        bool isDatePickerOpen;

        SfDateTimePicker? DateTimePiker;

        CultureInfo? currentCulture;

        [RelayCommand]
        void OpenDatePicker(SfDateTimePicker views) {

            if (views != null) {

                DateTimePiker = views;

                views.IsOpen = true;
            }

        }

        [RelayCommand]
        void ConfirmDate(DateTime dateTime) {

            if (DateTimePiker != null) {

                currentCulture = new CultureInfo(deviceLanguageService.GetDeviceLanguage());

                TurbneDate = dateTime;

                DateTimePiker.IsOpen = false;

                TurbineFormatedDate = TurbneDate?.ToString("g", currentCulture)!;
            }

        }

        [RelayCommand]
        void Cancel(SfDateTimePicker views) {

            if (views != null) {

                views.IsOpen = false;
            }

        }
    }
}
