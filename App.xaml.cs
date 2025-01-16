namespace METROWIND
{
    public partial class App: Application
    {
        public static MainWindow? WindowInstance { get; private set; }
        private readonly MainWindow _mainWindow;

        public App(MainWindow mainWindow)
        {
            SyncfusionLicenseProvider.RegisterLicense(AppConstants.SYNCFUSION_KEY);
            InitializeComponent();
            _mainWindow = mainWindow;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            WindowInstance = _mainWindow;
            return WindowInstance;
        }
    }
}
