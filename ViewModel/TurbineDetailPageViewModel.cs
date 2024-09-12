using METROWIND.Controls;

namespace METROWIND.ViewModel {

    [QueryProperty(nameof(SelectedPin), "SelectedPin")]
    public partial class TurbineDetailPageViewModel : ObservableObject {

        [ObservableProperty]
        CustomPin? selectedPin;

    }
}
