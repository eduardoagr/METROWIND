

using FFImageLoading.Maui;

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
            .UseFFImageLoading()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
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
            .WriteTo.File(Path.Combine(FileSystem.Current.AppDataDirectory, "Logs"),
            rollingInterval: RollingInterval.Day)
            .CreateLogger());

        services.AddSingleton(new BlobServiceClient(
            AppConstants.AZURE_CONNECTION_STRING));

        builder.Services.AddSingleton<HttpClient>();

        builder.Services.AddSingleton<IAppService, AppService>();
        builder.Services.AddSingleton<IBlobService, BlobService>();
        builder.Services.AddSingleton<IHttpService, HttpService>();
        builder.Services.AddSingleton<IFirestoreService, FirestoreService>();
        builder.Services.AddSingleton<ICommandHandler, TurbinesService>();
        builder.Services.AddSingleton<ITurbineService, TurbinesService>();
        builder.Services.AddSingleton<IDeviceLanguageService, DeviceLanguageService>();
        builder.Services.AddSingleton(Map.Default);
        builder.Services.AddSingleton(Geolocation.Default);
        builder.Services.AddSingleton(FilePicker.Default);
        builder.Services.AddSingleton(MediaPicker.Default);
        builder.Services.AddSingleton(Connectivity.Current);
        builder.Services.AddSingleton(Email.Default);
        builder.Services.AddSingleton(Share.Default);
        builder.Services.AddSingleton<DeviceLanguageService>();



        //Pages and ViewModels
        builder.Services.AddTransient<ChargingStationsMapPage, ChargingStationsMapPageViewModel>();
        builder.Services.AddTransient<TurbinesCollectionPage, TurbinesCollectionPageViewModel>();
        builder.Services.AddTransient<ArticleDetailsPage, ArticleDetailsPageViewModel>();

        builder.Services.AddTransient<AppShell, AppShellViewModel>();
        builder.Services.AddTransient<TurbineDetailPage, TurbineDetailPageViewModel>();
        builder.Services.AddTransient<NewsPage, NewsPageViewModel>();
        builder.Services.AddTransient<SupportPage, SupportPageViewModel>();

        builder.Services.AddSingleton<NoInternetPopUp>();

        builder.Services.AddSingleton<MainWindow>();

        return builder.Build();
    }
}