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
        private bool isInitializing = false;

        public ObservableCollection<TurbinePin> TurbinePins => _turbineService.TurbinePins;
        public ICommand PinClickedCommand => _turbineService.PinClickedCommand;

        public const string FLYOUT_KEY = "flyout_key";
        public const string SWITCH_KEY = "switch_key";

        [ObservableProperty]
        bool isLoadFinished;

        [ObservableProperty]
        bool isCompactMode;

        [ObservableProperty]
        bool isMenuPopUpOpen;

        public AppShellViewModel(ITurbineService turbineService, IAppService appService,
            IServiceProvider serviceProvider, ICommandHandler commandHandler,
            IConnectivity connectivity, NoInternetPopUp noInternetPopUp)
        {
            _serviceProvider = serviceProvider;
            _turbineService = turbineService;
            _appService = appService;
            _commandHandler = commandHandler;
            _connectivity = connectivity;
            _noInternetPopUp = noInternetPopUp;

            _turbineService.NoInternet += TurbineService_NoInternet;

            InitializeCommand();

            _turbineService.SetPinClickHandler(this);

            _connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void TurbineService_NoInternet()
        {
            _noInternetPopUp.Show();
        }

        private void InitializeCommand()
        {
            _commandHandler.SetPinClickedCommand(new Command<TurbinePin>(async (pin) =>
            await PinMarkerClicked(pin)));
        }

        [RelayCommand]
        async Task Appearing(AppShell appShell)
        {
            _shell = appShell;

            try
            {
                if (TurbinePins.Count == 0 && !isInitializing)
                {
                    isInitializing = true;
                    await _turbineService.InitializeAsync();
                    isInitializing = false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Initialization failed: {ex.Message}");
                isInitializing = false;
            }
        }

        private void Connectivity_ConnectivityChanged(object? sender, ConnectivityChangedEventArgs e)
        {
            HandleConnectivityChangeAsync();
        }

        private async void HandleConnectivityChangeAsync()
        {
            if (_connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                _noInternetPopUp.IsOpen = false;
                try
                {
                    if (TurbinePins.Count == 0 && !isInitializing)
                    {
                        isInitializing = true;
                        await _turbineService.InitializeAsync();
                        isInitializing = false;
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Initialization failed: {ex.Message}");
                    isInitializing = false;
                }
            }
            else
            {
                TurbineService_NoInternet();
            }
        }

        [RelayCommand]
        void OpenMenu()
        {
            IsMenuPopUpOpen = true;
        }

        [RelayCommand]
        void ToggleSwitch()
        {
            if (IsCompactMode)
            {
                _shell!.FlyoutWidth = 65;
            }
            else
            {
                _shell!.FlyoutWidth = 300;
            }

            IsMenuPopUpOpen = false;
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
