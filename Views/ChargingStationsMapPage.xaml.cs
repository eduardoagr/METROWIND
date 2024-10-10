namespace METROWIND.Views {

    public partial class ChargingStationsMapPage : ContentPage {

        public ChargingStationsMapPage(ChargingStationsMapPageViewModel chargingStationsMapPageViewModel) {
            InitializeComponent();
            BindingContext = chargingStationsMapPageViewModel;
        }
    }
}
