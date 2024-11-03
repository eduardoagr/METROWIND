namespace METROWIND.ViewModel {

    public partial class HomePageViewModel : ObservableObject {

        private readonly DeviceLanguageService deviceLanguageService;

        private readonly HttpService httpService;

        public Command<object> OnPinMarkerClickedCommand { get; set; }

        protected readonly TurbinesService _turbinesService;

        public ObservableCollection<TurbinePin> Turbines => _turbinesService.TurbinePins;

        [ObservableProperty]
        bool isLoading;

        public HomePageViewModel(HttpService service, DeviceLanguageService deviceLanguage, TurbinesService turbinesService) {

            IsLoading = true;

            deviceLanguageService = deviceLanguage;

            _turbinesService = turbinesService;

            httpService = service;

            OnPinMarkerClickedCommand = new Command<object>(OnPinMarkerClicked);

            _turbinesService = turbinesService;

            _turbinesService.GetTurbinePinsForUI(OnPinMarkerClickedCommand);

            LoadNews();

        }

        public ObservableCollection<Article>? NewsList { get; set; } =
            [];
        async void LoadNews() {

            var language = deviceLanguageService.GetDeviceLanguage();

            var newsUrl = AppConstants.GetNewsUrl(language);

            NewsList!.Clear();

            var newsObj = await httpService.GetAsync<News>(newsUrl);

            foreach (var item in newsObj!.Articles!) {
                NewsList.Add(item);
            }

            IsLoading = false;
        }


        [RelayCommand]
        protected void ShowNewsDetail(Article article) {
            if (article != null) {

                Shell.Current.GoToAsync($"{nameof(ArticleDetailsPage)}",
                    true,
                    new Dictionary<string, object> {

                        { "articleObj", article }
                    });
            }
        }

        void OnPinMarkerClicked(object turbine) {
            if (turbine != null) {
                // Handle the pin click event
                Shell.Current.GoToAsync($"{nameof(TurbineDetailPage)}",
                    true,
                    new Dictionary<string, object> {
                    { "SelectedTurbine", turbine }
                });
            };
        }
    }
}