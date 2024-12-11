using SelectionChangedEventArgs = Syncfusion.Maui.Inputs.SelectionChangedEventArgs;

namespace METROWIND.Views
{
    public partial class ChargingStationsMapPage: ContentPage
    {
        public ChargingStationsMapPageViewModel PageViewModel { get; }

        public ChargingStationsMapPage(ChargingStationsMapPageViewModel
            stationsMapPageViewModel)
        {
            InitializeComponent();
            PageViewModel = stationsMapPageViewModel;
            BindingContext = PageViewModel;

            InitializeMap();
            InitializeTitleBar();
            DeviceHelper.AddOrRemoveContentBasedOnDevice(MobileContent);
        }

        private void InitializeMap()
        {
            PageViewModel.MapView = ChargingStationMap;
            PageViewModel.MapDialogPopUp = MapChangeTypuPopUp;
        }

        private void InitializeTitleBar()
        {
            var tb = new AppTitleBar();
            tb.SetItemSource(PageViewModel.TurbinePins,
                "Turbine.Name", "Turbine.Name");
            App.WindowInstance!.TitleBar = tb;

            tb.ComboBox.SelectionChanged += ComboBox_SelectionChanged;
        }

        private async void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            if (sender is SfComboBox comboBox
                && comboBox.SelectedValue is TurbinePin selectedPin)
            {
                // Zoom into the selected turbine's location
                PageViewModel.ItemSelectedCommand.Execute(selectedPin.Turbine);

                // Wait for a short duration before navigation
                await Task.Delay(1000);

                // Navigate to the turbine detail page
                await PageViewModel.PinMarkerClicked(selectedPin);

                comboBox.SelectedItem = string.Empty;
            }
        }
    }
}
