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


        public static readonly BindableProperty CstomPinImagesProperty = BindableProperty.Create(
            nameof(CstomPinImages),
            typeof(IEnumerable),
            typeof(CustomPin));

        public IEnumerable CstomPinImages {
            get => (IEnumerable)GetValue(CstomPinImagesProperty);
            set => SetValue(CstomPinImagesProperty, value);
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
    }
}
