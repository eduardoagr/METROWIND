using Firebase.Auth.Providers;
using Firebase.Auth.Repository;

using Serilog;

namespace METROWIND {

    public static class MauiProgram {

        public static MauiApp CreateMauiApp() {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
#if WINDOWS
                .UseMauiCommunityToolkitMaps(AppConstants.BINGMAPS_APIKEY)
#elif ANDROID || IOS
           .UseMauiMaps()
#endif
                .UseMauiCommunityToolkit()
                .ConfigureSyncfusionCore()
                .ConfigureFonts(fonts => {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("fa-solid-900.ttf", "fa");
                    fonts.AddFont("MaterialIcons-Regular.ttf", "ma");
                }).ConfigureMauiHandlers(handlers => {

                    handlers.AddHandler<BorderlessEntry, EntryHandler>();

                }).ConfigureEssentials(essentials => {

                    essentials.UseMapServiceToken(AppConstants.BINGMAPS_APIKEY);

                });



            BorderlessEntryHandler.ApplyCustomHandler();



#if DEBUG
            builder.Logging.AddDebug();
#endif
            IServiceCollection services = builder.Services;

            services.AddSerilog(new LoggerConfiguration()
                .WriteTo.File(Path.Combine(FileSystem.Current.AppDataDirectory, "Logs"), rollingInterval: RollingInterval.Day)
                .CreateLogger());


            //Services
            builder.Services.AddSingleton(new FirebaseAuthClient(
                new FirebaseAuthConfig {

                    ApiKey = AppConstants.FIREBASEAUTHKEY,
                    AuthDomain = AppConstants.FIREBASEDOMAIN,
                    Providers = [
                    new EmailProvider()
                ],
                    UserRepository = new FileUserRepository(AppInfo.Name),

                }));

            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<HttpService>();
            builder.Services.AddSingleton<TurbinesService>();
            builder.Services.AddSingleton<GeoapifyService>();
            builder.Services.AddSingleton(Geolocation.Default);
            builder.Services.AddSingleton(FilePicker.Default);
            builder.Services.AddSingleton(MediaPicker.Default);
            builder.Services.AddSingleton(Connectivity.Current);
            builder.Services.AddSingleton<DeviceLanguageService>();



            //Pages and ViewModels
            builder.Services.AddTransient<ChargingStationsMapPage, ChargingStationsMapPageViewModel>();
            builder.Services.AddTransient<TurbinesCollectionPage, TurbinesCollectionPageViewModel>();
            builder.Services.AddTransient<ArticleDetailsPage, ArticleDetailsPageViewModel>();

            builder.Services.AddTransient<AddNewTurbinePage, AddNewTurbinePageViewModel>();
            builder.Services.AddSingleton<LoginPage, LoginPageViewModel>();

            builder.Services.AddSingleton<AppShell, AppShellViewModel>();
            builder.Services.AddSingleton<StartupPage, StartupPageViewModel>();
            builder.Services.AddSingleton<TurbineDetailPage, TurbineDetailPageViewModel>();
            builder.Services.AddSingleton<HomePage, HomePageViewModel>();

            return builder.Build();
        }
    }
}