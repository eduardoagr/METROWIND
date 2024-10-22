#if ANDROID
using Android.Widget;
#endif

namespace METROWIND.Handlers {
    public class BorderlessSearchBarHandler {
        public static void ApplyCustomHandler() {

            SearchBarHandler.Mapper.AppendToMapping("BorderlessSearchBar", (handler, view) => {
                if (view is BorderlessSearchBar) {
#if ANDROID
                    LinearLayout? linearLayout = handler.PlatformView.GetChildAt(0) as LinearLayout;
                    linearLayout = linearLayout?.GetChildAt(2) as LinearLayout;
                    linearLayout = linearLayout?.GetChildAt(1) as LinearLayout;
                    linearLayout!.Background = null;

#elif WINDOWS
                    handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#elif IOS || MACCATALYST

                    handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                    handler.PlatformView.SearchBarStyle = UIKit.UISearchBarStyle.Minimal;
#endif
                }
            });

        }
    }
}
