namespace METROWIND.ViewModel {

    [QueryProperty(nameof(SelectedTurbine), "SelectedTurbine")]
    public partial class TurbineDetailPageViewModel : ObservableObject {

        [ObservableProperty]
        Turbine? selectedTurbine;


        [RelayCommand]
        void Appear() {

            if (SelectedTurbine == null) {

                return;

            }

        }
    }
}
