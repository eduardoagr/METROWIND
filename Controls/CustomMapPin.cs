using System.Collections;

namespace METROWIND.Controls {

    public class CustomMapPin : Pin {

        public static readonly BindableProperty MarkerClickedCommandProperty =
            BindableProperty.Create(
                nameof(MarkerClickedCommand),
                typeof(ICommand),
                typeof(CustomMapPin),
                null);

        public ICommand? MarkerClickedCommand {
            get => (ICommand)GetValue(MarkerClickedCommandProperty);
            set => SetValue(MarkerClickedCommandProperty, value);
        }

        public static readonly BindableProperty MarkerClickedCommandParameterProperty = BindableProperty.Create(
            nameof(MarkerClickedCommandParameter), typeof(Turbine), typeof(CustomMapPin));

        public Turbine MarkerClickedCommandParameter {
            get => (Turbine)GetValue(MarkerClickedCommandParameterProperty);
            set => SetValue(MarkerClickedCommandParameterProperty, value);
        }

        public static readonly BindableProperty CustomPinLabelProperty = BindableProperty.Create(
            nameof(CustomPinLabel), typeof(string), typeof(CustomMapPin));

        public string CustomPinLabel {
            get => (string)GetValue(CustomPinLabelProperty);
            set => SetValue(CustomPinLabelProperty, value);
        }

        public static readonly BindableProperty CustomPinAddressProperty = BindableProperty.Create(
            nameof(CustomPinAddress), typeof(string), typeof(CustomMapPin));

        public string CustomPinAddress {
            get => (string)GetValue(CustomPinAddressProperty);
            set => SetValue(CustomPinAddressProperty, value);
        }

        public static readonly BindableProperty TurbineImagesProperty = BindableProperty.Create(
            nameof(TurbineImages),
            typeof(IEnumerable),
            typeof(CustomMapPin));


        public IEnumerable TurbineImages {
            get => (IEnumerable)GetValue(TurbineImagesProperty);
            set => SetValue(TurbineImagesProperty, value);
        }

        public static readonly BindableProperty ChartDataProperty = BindableProperty.Create(
            nameof(ChartData), typeof(IEnumerable),
            typeof(CustomMapPin));

        public IEnumerable ChartData {
            get => (IEnumerable)GetValue(ChartDataProperty);
            set => SetValue(ChartDataProperty, value);
        }


        public CustomMapPin() {
            MarkerClicked += CustomPin_MarkerClicked;
        }

        private void CustomPin_MarkerClicked(object? sender, PinClickedEventArgs e) {
            // Ensure info window is shown
            e.HideInfoWindow = true;

            // Execute command if any
            if (MarkerClickedCommand?.CanExecute(MarkerClickedCommandParameter) == true) {
                MarkerClickedCommand.Execute(MarkerClickedCommandParameter);
            }
        }
    }
}
