using Microsoft.Maui.Maps;

using Map = Microsoft.Maui.Controls.Maps.Map;

namespace METROWIND.Controls {
    public class MvvmMap : Map {

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(
            nameof(SelectedItem), typeof(object), typeof(MvvmMap),
            null, BindingMode.TwoWay);

        public object SelectedItem {
            get => GetValue(SelectedItemProperty);
            set => SetValue(SelectedItemProperty, value);
        }


        public static readonly BindableProperty MapSpanProperty = BindableProperty.Create(
            nameof(MapSpan), typeof(MapSpan), typeof(MvvmMap),
            null, BindingMode.TwoWay,
            propertyChanged: OnMapSpanPropertyChanged);


        public MapSpan MapSpan {
            get => (MapSpan)GetValue(MapSpanProperty);
            set => SetValue(MapSpanProperty, value);
        }


        private static void OnMapSpanPropertyChanged(BindableObject bindable, object oldValue,
            object newValue) {

            if (bindable is MvvmMap map && newValue is MapSpan mapSpan) {
                MoveMap(map, mapSpan);
            }
        }

        private static void MoveMap(MvvmMap map, MapSpan mapSpan) {

            var timer = Application.Current!.Dispatcher.CreateTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += (s, e) => {
                if (s is IDispatcherTimer timer) {
                    timer.Stop();

                    MainThread.BeginInvokeOnMainThread(() => map.MoveToRegion(mapSpan));
                }
            };

            timer.Start();
        }
    }
}