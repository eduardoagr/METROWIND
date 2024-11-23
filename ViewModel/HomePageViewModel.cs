namespace METROWIND.ViewModel
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly DeviceLanguageService deviceLanguageService;

        private readonly IServiceProvider provider;

        private readonly HttpService httpService;

        public Command<object> OnPinMarkerClickedCommand { get; set; }

        protected readonly TurbinesService _turbinesService;


        public ObservableCollection<TurbinePin> Turbines => _turbinesService.TurbinePins;

        public HomePageViewModel(HttpService service, DeviceLanguageService
            deviceLanguage, TurbinesService turbinesService, IServiceProvider serviceProvider)
        {
            deviceLanguageService = deviceLanguage;

            httpService = service;

            LoadNews();

            provider = serviceProvider;

            _turbinesService = turbinesService;

            OnPinMarkerClickedCommand = new Command<object>(OnPinMarkerClicked);

            _turbinesService.GetTurbinePinsForUI(OnPinMarkerClickedCommand);

        }

        public ObservableCollection<Article>? NewsList { get; set; } =
            [];

        async void LoadNews()
        {

            var language = deviceLanguageService.GetDeviceCultureInfo();

            var newsUrl = AppConstants.GetNewsUrl(language.TwoLetterISOLanguageName);

            NewsList!.Clear();

            var newsObj = await httpService.GetAsync<News>(newsUrl);

            foreach (var item in newsObj!.Articles!)
            {
                NewsList.Add(item);
            }
        }


        [RelayCommand]
        protected void ShowNewsDetail(Article article)
        {
            if (article != null)
            {

                Shell.Current.GoToAsync($"{nameof(ArticleDetailsPage)}",
                    true,
                    new Dictionary<string, object> {

                        { "articleObj", article }
                    });
            }
        }

        void OnPinMarkerClicked(object turbine)
        {
            if (turbine != null)
            {
#if ANDROID || IOS
                Shell.Current.GoToAsync($"{nameof(TurbineDetailPage)}",
                     true,
                     new Dictionary<string, object> {
                    { "SelectedTurbine", turbine }
                });
#elif WINDOWS || MACCATALYST

                var viewModel = provider.GetRequiredService<TurbineDetailPageViewModel>();
                viewModel.SelectedTurbine = (TurbinePin)turbine;

                var page = provider.GetRequiredService<TurbineDetailPage>();
                page.BindingContext = viewModel;

                var secondWindow = new Window(page);

                var existingWindow = Application.Current!.Windows.FirstOrDefault(
                    w => w.Page is TurbineDetailPage detailPage);

                if (existingWindow != null)
                {
                    Application.Current?.ActivateWindow(existingWindow);
                }
                else
                {
                    Application.Current!.OpenWindow(secondWindow);
                }
#endif
            };
        }
    }
}