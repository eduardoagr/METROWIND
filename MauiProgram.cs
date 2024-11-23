using Map = Microsoft.Maui.ApplicationModel.Map;

namespace METROWIND;

public static class MauiProgram
{

    public static MauiApp CreateMauiApp()
    {
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
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("fa-solid-900.ttf", "fa");
                fonts.AddFont("MaterialIcons-Regular.ttf", "ma");
            }).ConfigureMauiHandlers(handlers =>
            {
                handlers.AddHandler<BorderlessEntry, EntryHandler>();

            }).ConfigureEssentials(essentials =>
            {
                essentials.UseMapServiceToken(AppConstants.BINGMAPS_APIKEY);

            }).ConfigureMauiHandlers(handler =>
            {

                handler.AddHandler<BorderlessEditor, EditorHandler>();
            });

        BorderlessEntryHandler.ApplyCustomHandler();
        BorderlesEditorHandler.ApplyCustomHandler();

#if DEBUG
        builder.Logging.AddDebug();
#endif
        IServiceCollection services = builder.Services;

        services.AddSerilog(new LoggerConfiguration()
            .WriteTo.File(Path.Combine(FileSystem.Current.AppDataDirectory, "Logs"), rollingInterval: RollingInterval.Day)
            .CreateLogger());


        builder.Services.AddSingleton<HttpClient>();
        builder.Services.AddSingleton<FirestoreService>();
        builder.Services.AddSingleton<HttpService>();
        services.AddSingleton(new BlobServiceClient(AppConstants.AZURE_CONNECTION_STRING));
        builder.Services.AddSingleton<TurbinesService>();
        builder.Services.AddSingleton(Map.Default);
        builder.Services.AddSingleton(Geolocation.Default);
        builder.Services.AddSingleton(FilePicker.Default);
        builder.Services.AddSingleton(MediaPicker.Default);
        builder.Services.AddSingleton(Connectivity.Current);
        builder.Services.AddSingleton(Email.Default);
        builder.Services.AddSingleton<DeviceLanguageService>();



        //Pages and ViewModels
        builder.Services.AddTransient<ChargingStationsMapPage, ChargingStationsMapPageViewModel>();
        builder.Services.AddTransient<TurbinesCollectionPage, TurbinesCollectionPageViewModel>();
        builder.Services.AddTransient<ArticleDetailsPage, ArticleDetailsPageViewModel>();

        builder.Services.AddSingleton<AppShell, AppShellViewModel>();
        builder.Services.AddTransient<TurbineDetailPage, TurbineDetailPageViewModel>();
        builder.Services.AddSingleton<HomePage, HomePageViewModel>();
        builder.Services.AddTransient<SupportPage, SupportPageViewModel>();

        return builder.Build();
    }
}