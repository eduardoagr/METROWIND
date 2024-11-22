#if ANDROID
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace METROWIND.Handlers
{

    public static class BorderlessEntryHandler
    {

        public static void ApplyCustomHandler()
        {

            EntryHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
#if ANDROID
                handler.PlatformView.Background = null;
                handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
                handler.PlatformView.BackgroundTintList = Android.Content.Res.ColorStateList.ValueOf(Colors.Transparent.ToAndroid());
#elif IOS
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
#elif MACCATALYST
                handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                handler.PlatformView.Layer.BorderWidth = 0;
#elif WINDOWS
                handler.PlatformView.Background = null;
                handler.PlatformView.BorderThickness = new Microsoft.UI.Xaml.Thickness(0);
#endif
            });
        }
    }
}
