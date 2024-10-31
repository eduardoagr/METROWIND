namespace METROWIND.ViewModel;

public partial class AddNewTurbinePageViewModel(TurbinesService turbinesService,
    DeviceLanguageService deviceLanguageService, IFilePicker filePicker,
    IMediaPicker mediaPicker, IGeolocation geolocation, ILogger<AddNewTurbinePage> logger) :
    ChargingStationsMapPageViewModel(turbinesService) {

    [ObservableProperty]
    Turbine turbine = new();

    CultureInfo? currentCulture;

    private FirestoreDb? firestoreDb;

    [RelayCommand]
    void PageEnter() {

        InitializeAsync();

    }

    private async void InitializeAsync() {

    }

    [RelayCommand]
    async Task NavigateBack() {

        await Shell.Current.GoToAsync("..", true);
    }


    [RelayCommand]
    void OpenDatePicker(SfDateTimePicker datePicker) {

        if (datePicker != null) {

            datePicker.IsOpen = true;
        }

    }

    [RelayCommand]
    void ConfirmDate(SfDateTimePicker datePicker) {

        if (datePicker != null) {

            currentCulture = new CultureInfo(deviceLanguageService.GetDeviceLanguage());

            Turbine!.InstalationDateTime = datePicker.SelectedDate;

            datePicker.IsOpen = false;

            Turbine.StringifyInstalationDate = Turbine.InstalationDateTime?.ToString(
                "D", currentCulture)!;

        }
    }

    [RelayCommand]
    void CancelDate(SfDateTimePicker views) {

        if (views != null) {

            views.IsOpen = false;
        }
    }


    async Task<Location?> GetLocation(string address) {

        try {
            IEnumerable<Location> locations = await Geocoding.Default.GetLocationsAsync(address);
            return locations?.FirstOrDefault();
        }
        catch {
            // Handle the exception (e.g., log it or show an error message to the user)
            await Shell.Current.DisplayAlert("Error",
                Resources.AppResource.AddressFound, "OK");
            return null;
        }
    }


    [RelayCommand]
    async Task PickImages() {

        var results = await filePicker.PickMultipleAsync(new PickOptions {

            FileTypes = FilePickerFileType.Images,
        });

        if (results != null) {
            foreach (var result in results) {

                var fileData = new FileData {
                    FileName = result.FileName,
                    FullPath = result.FullPath
                };

                if (!Turbine.Images!.Contains(fileData)) {
                    Turbine.Images.Add(fileData);
                }
            }

        }
    }

    [RelayCommand]
    async Task TakePhoto() {
        var photo = await mediaPicker.CapturePhotoAsync();
        if (photo != null) {
            using Stream sourceStream = await photo.OpenReadAsync();

#if WINDOWS || MACCATALYST
        string localDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures), AppInfo.Name);
        if (!Directory.Exists(localDirectoryPath))
        {
            Directory.CreateDirectory(localDirectoryPath);
        }
        string localFilePath = Path.Combine(localDirectoryPath, photo.FileName);
        using FileStream localFileStream = File.Create(localFilePath);
        await sourceStream.CopyToAsync(localFileStream);
        localFileStream.Close();

#elif ANDROID || IOS
            using MemoryStream ms = new();
            await sourceStream.CopyToAsync(ms);
            var b = ms.ToArray();

#if ANDROID
            Platforms.Android.SavePictureService.SavePicture(b, photo.FileName);
#elif IOS
            Platforms.iOS.SavePictureService.SavePicture(b, photo.FileName);
#endif

#endif
        }
    }

    [RelayCommand]
    void OpenPopUp(SfPopup views) {

        if (views != null) {
            views.IsOpen = true;
        }
    }

    [RelayCommand]
    void ClosePopUp(SfPopup views) {

        if (views != null) {
            views.IsOpen = false;
        }
    }




    [RelayCommand]
    async Task SaveAndClose() {

        var turbineLocation = await GetLocation(Turbine!.Address!);

        if (turbineLocation != null) {

            _turbinesService.AddTurbinePin(new TurbinePin {

                Turbine = new Turbine {
                    Name = Turbine.Name,
                    Address = Turbine.Address,
                    StringifyInstalationDate = Turbine.StringifyInstalationDate,
                    Images = Turbine.Images,
                    Location = turbineLocation
                },
            }, OnPinMarkerClickedCommand!);

            await Shell.Current.GoToAsync("..", true);
        }
    }

    [RelayCommand]
    public async Task GetCurrentLocation() {

        var location = await geolocation.GetLastKnownLocationAsync() ??
            await geolocation.GetLocationAsync(new GeolocationRequest {
                DesiredAccuracy = GeolocationAccuracy.High,
                Timeout = TimeSpan.FromSeconds(30)
            });

        var placemarks = await Geocoding.GetPlacemarksAsync(location!.Latitude, location!.Longitude);
        var placemark = placemarks.FirstOrDefault();
        Turbine.Address = placemark?.FeatureName;
    }
}