#if ANDROID
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls.Compatibility.Platform.Android;
#endif

namespace METROWIND.Handlers
{
    public static class BorderlesEditorHandler
    {
        public static void ApplyCustomHandler()
        {

            EditorHandler.Mapper.AppendToMapping("Borderless", (handler, view) =>
            {
                if (view is BorderlessEditor)
                {
#if ANDROID
                    handler.PlatformView.Background = null;
                    handler.PlatformView.SetBackgroundColor(Android.Graphics.Color.Transparent);
#elif IOS || MACCATALYST
                    handler.PlatformView.BackgroundColor = UIKit.UIColor.Clear;
                    handler.PlatformView.Layer.BorderWidth = 0;
#endif
                }

            });
        }
    }
}

