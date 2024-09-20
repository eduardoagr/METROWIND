using METROWIND.Controls;

namespace METROWIND.ViewModel {

    [QueryProperty(nameof(SelectedTurbine), "SelectedTurbine")]
    public partial class TurbineDetailPageViewModel : ObservableObject {

        [ObservableProperty]
        CustomPin? selectedTurbine;

    }
}
