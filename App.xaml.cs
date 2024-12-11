namespace METROWIND
{
    public partial class App: Application
    {
        public static MainWindow? WindowInstance { get; private set; }

        private readonly AppShell _shell;
        private readonly MainWindow _mainWindow;

        public App(MainWindow mainWindow, AppShell shell)
        {
            SyncfusionLicenseProvider.RegisterLicense(AppConstants.SYNCFUSION_KEY);
            InitializeComponent();
            _mainWindow = mainWindow;
            _shell = shell;

        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            WindowInstance = _mainWindow;
            return WindowInstance;
        }
    }
}
