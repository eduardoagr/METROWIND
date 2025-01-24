using System.Collections.Concurrent;

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

            // Initialize the news asynchronously
            InitializeAsync().ConfigureAwait(false);
        }

        private async Task InitializeAsync()
        {
            await LoadNewsAsync();
        }

        public async Task LoadNewsAsync()
        {
            var language = _deviceLanguageService.GetDeviceCultureInfo();
            var newsUrl = AppConstants.GetNewsUrl(language.TwoLetterISOLanguageName);

            var newsObj = await _httpService.GetAsync<News>(newsUrl);

            if (newsObj != null && newsObj.Articles != null)
            {
                var concurrentBag = new ConcurrentBag<Article>();

                var tasks = newsObj.Articles.Select(async article =>
                {
                    // Any async operation on the article can be done here
                    concurrentBag.Add(article);
                    await Task.CompletedTask;
                });

                await Task.WhenAll(tasks);

                foreach (var article in concurrentBag)
                {
                    ArticleList.Add(article);
                }
            }
        }


        [RelayCommand]
        public async Task ShowNewsDetail(string url)
        {
            if (!string.IsNullOrEmpty(url))
            {
                await _appService.NavigateToPage($"{nameof(ArticleDetailsPage)}",
                    new Dictionary<string, object> { { "articleURL", url } }
                );
            }
        }
    }
}
