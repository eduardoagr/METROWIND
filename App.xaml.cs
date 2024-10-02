namespace METROWIND {
    public partial class App : Application {

        private readonly TurbinesService turbinesService;

        public App(AppShell appShell, TurbinesService _turbinesService) {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(AppConstants.SYNCFUSION_KEY);

            InitializeComponent();

            MainPage = appShell;

            turbinesService = _turbinesService;
        }

        protected override void OnStart() {
            base.OnStart();
            // Start the incrementation logic here
            turbinesService.StartTimer();
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            // Handle when your app resumes
        }
    }
}
