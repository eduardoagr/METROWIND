namespace METROWIND.Controls {
    internal class CustomImage : Image {

        public static readonly BindableProperty ErrorImageSourceProperty = BindableProperty.Create(
            nameof(ErrorImageSource),
            typeof(ImageSource),
            typeof(CustomImage));

        public ImageSource ErrorImageSource {
            get => (ImageSource)GetValue(ErrorImageSourceProperty);
            set => SetValue(ErrorImageSourceProperty, value);
        }

        public CustomImage() {
            // Subscribe to the PropertyChanged event
            this.PropertyChanged += OnPropertyChanged;
        }

        private void OnPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e) {
            if (e.PropertyName == nameof(Source)) {

                if (Source != null && this.IsLoading) {

                    Dispatcher.StartTimer(TimeSpan.FromSeconds(5), () => {
                        if (IsLoading == false) {

                            HandleLoadingFailed();
                        }
                        return false;
                    });
                }
            }
        }

        private void HandleLoadingFailed() {
            // If loading fails, set the source to the error image
            if (ErrorImageSource != null) {
                Source = ErrorImageSource;
            }
        }
    }
}

