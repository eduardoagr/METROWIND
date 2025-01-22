namespace METROWIND.ViewModel
{
    public partial class NewsPageViewModel: ObservableObject
    {
        public ObservableCollection<Article> ArticleList { get; set; } = [];

        readonly IHttpService _httpService;
        readonly IDeviceLanguageService _deviceLanguageService;
        readonly IAppService _appService;

        public NewsPageViewModel(IDeviceLanguageService deviceLanguageService,
            IHttpService httpService,
            IAppService appService)
        {
            _deviceLanguageService = deviceLanguageService;
            _httpService = httpService;
            _appService = appService;
            LoadNews();

        }

        public async void LoadNews()
        {
            var language = _deviceLanguageService.GetDeviceCultureInfo();
            var newsUrl = AppConstants.GetNewsUrl(language.TwoLetterISOLanguageName);

            var newsObj = await _httpService.GetAsync<News>(newsUrl);

            if (newsObj != null && newsObj.Articles != null)
            {
                var tasks = newsObj.Articles.Select(async article =>
                {
                    // Any async operation on the article can be done here
                    ArticleList.Add(article);
                });

                await Task.WhenAll(tasks);
            }
        }


        [RelayCommand]
        async Task ShowNewsDetail(string Url)
        {
            if (!string.IsNullOrEmpty(Url))
            {
                Debug.WriteLine($"Navigating to ArticleDetailsPage with URL: {Url}");
                await _appService.NavigateToPage($"{nameof(ArticleDetailsPage)}",
                    new Dictionary<string, object> { { "articleURL", Url } }
                );
            }
        }

    }
}
