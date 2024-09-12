global using CommunityToolkit.Maui;
global using CommunityToolkit.Maui.Alerts;
global using CommunityToolkit.Maui.Core;
global using CommunityToolkit.Maui.Maps;
global using CommunityToolkit.Mvvm.ComponentModel;
global using CommunityToolkit.Mvvm.Input;

global using METROWIND.Constants;
global using METROWIND.Models;
global using METROWIND.Services;
global using METROWIND.ViewModel;
global using METROWIND.Views;

global using Microsoft.Extensions.Logging;
global using Microsoft.Maui.Controls.Maps;

global using SkiaSharp.Views.Maui.Controls.Hosting;

global using SQLite;

global using Syncfusion.Maui.Core.Hosting;

global using System.Collections.ObjectModel;
global using System.Diagnostics;
global using System.Net.Http.Json;
global using System.Text.Json;
global using System.Windows.Input;

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
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            //Services
            builder.Services.AddSingleton<HttpClient>();
            builder.Services.AddSingleton<HttpService>();
            builder.Services.AddSingleton(Connectivity.Current);


            //Pages and ViewModels
            builder.Services.AddSingleton<AppShell, AppShellViewModel>();
            builder.Services.AddSingleton<StartupPage, StartupPageViewModel>();
            builder.Services.AddSingleton<ChargingStationsMapPage, ChargingStationsMapPageViewModel>();
            builder.Services.AddSingleton<TurbineDetailPage, TurbineDetailPageViewModel>();
            builder.Services.AddSingleton<ArticleDetailsPage, ArticleDetailsPageViewModel>();
            builder.Services.AddTransient<HomePage, HomePageViewModel>();

            return builder.Build();
        }
    }
}