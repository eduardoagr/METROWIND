using System.Collections;

namespace METROWIND.Controls {

    public class CustomPin : Pin {

        public static readonly BindableProperty MarkerClickedCommandProperty =
            BindableProperty.Create(
                nameof(MarkerClickedCommand),
                typeof(ICommand),
                typeof(CustomPin),
                null);

        public ICommand? MarkerClickedCommand {
            get => (ICommand)GetValue(MarkerClickedCommandProperty);
            set => SetValue(MarkerClickedCommandProperty, value);
        }


        public static readonly BindableProperty CustomPinLabelProperty = BindableProperty.Create(
            nameof(CustomPinLabel), typeof(string), typeof(CustomPin));

        public string CustomPinLabel {
            get => (string)GetValue(CustomPinLabelProperty);
            set => SetValue(CustomPinLabelProperty, value);
        }


        public static readonly BindableProperty CustomPinAddressProperty = BindableProperty.Create(
            nameof(CustomPinAddress), typeof(string), typeof(CustomPin));

        public string CustomPinAddress {
            get => (string)GetValue(CustomPinAddressProperty);
            set => SetValue(CustomPinAddressProperty, value);
        }


        public static readonly BindableProperty TurbineImagesProperty = BindableProperty.Create(
            nameof(TurbineImages),
            typeof(IEnumerable),
            typeof(CustomPin));

        public IEnumerable TurbineImages {
            get => (IEnumerable)GetValue(TurbineImagesProperty);
            set => SetValue(TurbineImagesProperty, value);
        }


        public CustomPin() {
            MarkerClicked += CustomPin_MarkerClicked;
        }

        private void CustomPin_MarkerClicked(object? sender, PinClickedEventArgs e) {
            // Ensure info window is shown
            e.HideInfoWindow = true;

            // Execute command if any
            if (MarkerClickedCommand?.CanExecute(this) == true) {
                MarkerClickedCommand.Execute(this);
            }
        }


        public static readonly BindableProperty ChartDataProperty = BindableProperty.Create(
            nameof(ChartData), typeof(IEnumerable),
            typeof(CustomPin));

        public IEnumerable ChartData {
            get => (IEnumerable)GetValue(ChartDataProperty);
            set => SetValue(ChartDataProperty, value);
        }

    }
}
