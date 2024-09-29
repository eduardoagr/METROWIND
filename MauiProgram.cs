using METROWIND.Controls;
using METROWIND.Views.PopUps;

using Microsoft.Maui.Handlers;

namespace METROWIND {

    public static class MauiProgram {

        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
#if WINDOWS
                .UseMauiCommunityToolkitMaps("AlKrbMWDZLkrL510ErB0LyjdmGrPsKiDayOH6V8tSBmlJYdQufZFtTnY_DEZG2xf")
#elif ANDROID || IOS
           .UseMauiMaps()
#endif
                .UseSkiaSharp()
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "fa");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "ma");
                }).ConfigureMauiHandlers(handlers => {

                    handlers.AddHandler<BorderlessEntry, EntryHandler>();
                });

            BorderlessEntryHandler.ApplyCustomHandler();



#if DEBUG
            builder.Logging.AddDebug();
#endif
            //Services
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<HttpService>();
            builder.Services.AddSingleton<TurbinesService>();
            builder.Services.AddSingleton<GeoapifyService>();
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton<DeviceLanguageService>();



            //Pages and ViewModels
            builder.Services.AddTransientPopup<AddTurbnePopUp, AddTurbnePopUpViewModel>();
            builder.Services.AddTransient<ChargingStationsMapPage, ChargingStationsMapPageViewModel>();
            builder.Services.AddTransient<TurbinesCollectionPage, TurbinesCollectionPageViewModel>();
            builder.Services.AddTransient<ArticleDetailsPage, ArticleDetailsPageViewModel>();


            builder.Services.AddSingleton<AppShell, AppShellViewModel>();
            builder.Services.AddSingleton<StartupPage, StartupPageViewModel>();
            builder.Services.AddSingleton<TurbineDetailPage, TurbineDetailPageViewModel>();
            builder.Services.AddSingleton<HomePage, HomePageViewModel>();
                                                                                                     
            return builder.Build();
        }
    }
}