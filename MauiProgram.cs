namespace METROWIND {

    public static class MauiProgram {

        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
#if WINDOWS
                .UseMauiCommunityToolkitMaps("2MJcwb3sDhOi7KnZYZFz~kBuOXKu5oDgZhJzhgiR6Tg~Akm14AZMJcKfXhu0JgJPCOuYTWsnRF3VWJ91UX0_nHRYa4zl082ffWsy7DV-id6a")
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
                });


#if DEBUG
            builder.Logging.AddDebug();
#endif

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<HttpService>();
            builder.Services.AddSingleton<DeviceLanguageService>();
            builder.Services.AddSingleton(Dispatcher.GetForCurrentThread()!);
            builder.Services.AddSingleton<TurbinesService>();
            builder.Services.AddSingleton(Connectivity.Current);


            //Pages and ViewModels
            builder.Services.AddSingleton<AppShell, AppShellViewModel>();
            builder.Services.AddSingleton<StartupPage, StartupPageViewModel>();
            builder.Services.AddSingleton<ChargingStationsMapPage, ChargingStationsMapPageViewModel>();
            builder.Services.AddSingleton<TurbineDetailPage, TurbineDetailPageViewModel>();
            builder.Services.AddSingleton<TurbinesCollectionPage, TurbinesCollectionPageViewModel>();
            builder.Services.AddTransient<ArticleDetailsPage, ArticleDetailsPageViewModel>();
            builder.Services.AddSingleton<HomePage, HomePageViewModel>();

            return builder.Build();
        }
    }
}