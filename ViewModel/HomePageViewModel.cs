namespace METROWIND.ViewModel
{
    public partial class HomePageViewModel : ObservableObject
    {
        private readonly DeviceLanguageService deviceLanguageService;

        private readonly HttpService httpService;

        public Command<object> OnPinMarkerClickedCommand { get; set; }

        protected readonly TurbinesService _turbinesService;

        public ObservableCollection<TurbinePin> Turbines => _turbinesService.TurbinePins;

        public HomePageViewModel(HttpService service, DeviceLanguageService deviceLanguage, TurbinesService turbinesService)
        {
            deviceLanguageService = deviceLanguage;

            httpService = service;

            LoadNews();

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

                Shell.Current.GoToAsync($"{nameof(TurbineDetailPage)}",
                     true,
                     new Dictionary<string, object> {
                    { "SelectedTurbine", turbine }
                });
            };
        }
    }
}