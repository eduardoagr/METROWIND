using CommunityToolkit.Maui;
using CommunityToolkit.Maui.Maps;

using METROWIND.ViewModel;
using METROWIND.Views;

using Microsoft.Extensions.Logging;

namespace METROWIND {
    public static class MauiProgram {
        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
#if WINDOWS
        .UseMauiCommunityToolkitMaps("AlKrbMWDZLkrL510ErB0LyjdmGrPsKiDayOH6V8tSBmlJYdQufZFtTnY_DEZG2xf")
#elif ANDROID || IOS
                .UseMauiMaps()
#endif
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<AppShell>();
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainPageViewModel>();

            return builder.Build();
        }
    }
}
