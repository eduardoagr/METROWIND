
namespace METROWIND.ViewModel
{
    public partial class AppShellViewModel: ObservableObject, IPinClickHandler
    {
        private AppShell? _shell;
        private NoInternetPopUp _noInternetPopUp;
        private readonly IServiceProvider _serviceProvider;
        private readonly ITurbineService _turbineService;
        private readonly ICommandHandler _commandHandler;
        private readonly IConnectivity _connectivity;
        private readonly IAppService _appService;

        public ObservableCollection<TurbinePin> TurbinePins => _turbineService.TurbinePins;
        public ICommand PinClickedCommand => _turbineService.PinClickedCommand;

        public const string FLYOUT_KEY = "flyouy_key";
        public const string SWITCH_KEY = "switch_key";

        [ObservableProperty]
        bool isLoadFinished;

        [ObservableProperty]
        bool isCompactMode;

        [ObservableProperty]
        bool isMenuPopUpOen;

        public AppShellViewModel(ITurbineService turbineService, IAppService appService,
            IServiceProvider serviceProvider, ICommandHandler commandHandler,
            IConnectivity connectivity, NoInternetPopUp noInternetPopUp)
        {
            _serviceProvider = serviceProvider;
            _turbineService = turbineService;
            _appService = appService;
            _commandHandler = commandHandler;

            // Set the service command to execute the ViewModel method
            _commandHandler.SetPinClickedCommand(new Command<TurbinePin>(async (pin) =>
            await PinMarkerClicked(pin))); // Ensure the service knows this ViewModel as the handler

            _turbineService.SetPinClickHandler(this);
            _connectivity = connectivity;
            _noInternetPopUp = noInternetPopUp;
        }

        [RelayCommand]
        async void Appearing(AppShell appShell)
        {
            _shell = appShell;

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _noInternetPopUp.Show();
            }
            else if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                await _turbineService.InitializeAsync();

                _commandHandler.SetPinClickedCommand(PinClickedCommand);
            }

            _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            HandleConnectivityChangeAsync();
        }

        [RelayCommand]
        void OpenMenu()
        {
            IsMenuPopUpOen = true;
        }

        [RelayCommand]
        void ToogleSwitch()
        {

            if (IsCompactMode)
            {
                _shell!.FlyoutWidth = 65;
            }
            else
            {
                _shell!.FlyoutWidth = 300;
            }

            IsMenuPopUpOen = false;

        }

        private void HandleConnectivityChangeAsync()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _noInternetPopUp.Show();
            }
            else if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (_noInternetPopUp.IsOpen)
                {
                    _noInternetPopUp.IsOpen = false;
                }

                Application.Current!.Windows[0].Page = new AppShell(this);
            }

        }

        public async Task PinMarkerClicked(TurbinePin turbine)
        {
            if (turbine != null)
            {
#if ANDROID || IOS
                await _appService.NavigateToPage(nameof(TurbineDetailPage),
            new Dictionary<string, object> { { "SelectedTurbine", turbine } });
#elif WINDOWS || MACCATALYST
                var viewModel = _serviceProvider.GetRequiredService<TurbineDetailPageViewModel>();

                viewModel.SelectedTurbine = turbine;

                var page = _serviceProvider.GetRequiredService<TurbineDetailPage>();

                page.BindingContext = viewModel;

                var secondWindow = new Window(page);

                var existingWindow = Application.Current!.Windows.FirstOrDefault(
                    w => w.Page is TurbineDetailPage);

                if (existingWindow != null)
                {
                    Application.Current?.ActivateWindow(existingWindow);
                }
                else
                {
                    Application.Current!.OpenWindow(secondWindow);
                }
#endif
            }
        }
    }
}

