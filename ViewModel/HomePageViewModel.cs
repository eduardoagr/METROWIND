namespace METROWIND.ViewModel {

    public partial class HomePageViewModel(HttpService httpService,
        DeviceLanguageService deviceLanguageService) : ObservableObject {


        [ObservableProperty]
        bool isLoading;

        public ObservableCollection<Article>? NewsList { get; set; } =
            [];

        [RelayCommand]
        void Appearing(CollectionView collectionView) {

            IsLoading = true;

            LoadNews();
        }
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
    }
}