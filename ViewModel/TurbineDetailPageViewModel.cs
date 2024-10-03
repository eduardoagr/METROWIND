using CommunityToolkit.Maui.Core;

using Syncfusion.Maui.Popup;

namespace METROWIND.ViewModel {

    [QueryProperty(nameof(SelectedTurbine), "SelectedTurbine")]
    public partial class TurbineDetailPageViewModel(IPopupService popupService) : ObservableObject {

        [ObservableProperty]
        Turbine? selectedTurbine;

        [ObservableProperty]
        ImageSource imageSource;

        SfPopup popUp;


        [RelayCommand]
        void Appear(SfPopup views) {

            popUp = views;

        }

        [RelayCommand]
        void OpenPicture(string image) {

            ImageSource = image;

            popUp.IsOpen = true;
        }
    }
}
