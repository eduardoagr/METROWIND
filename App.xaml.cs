namespace METROWIND {
    public partial class App : Application {

        public App(AppShell appShell) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppConstants.SYNCFUSION_KEY);

            InitializeComponent();

            MainPage = appShell;
        }
    }
}
