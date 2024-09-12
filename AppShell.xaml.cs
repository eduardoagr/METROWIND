namespace METROWIND {
    public partial class AppShell : Shell {
        public AppShell(AppShellViewModel appShellViewModel) {
            InitializeComponent();
            BindingContext = appShellViewModel;

            Routing.RegisterRoute(nameof(TurbineDetailPage),
                typeof(TurbineDetailPage));

            Routing.RegisterRoute(nameof(ArticleDetailsPage),
                typeof(ArticleDetailsPage));
        }
    }
}
