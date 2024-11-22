namespace METROWIND
{
    public partial class App : Application
    {
        AppShell shell;

        public App(AppShell appShell)
        {

            SyncfusionLicenseProvider.RegisterLicense(AppConstants.SYNCFUSION_KEY);

            InitializeComponent();

            shell = appShell;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            Window window = new MainWindow(shell);

            return window;

        }
    }
}
