namespace METROWIND.ViewModel {

    public partial class HomePageViewModel(HttpService httpService,
        DeviceLanguageService deviceLanguageService) : ObservableObject {

        private CollectionView? NewsCollectionView;

        private int scrollToPosition;

        [ObservableProperty]
        bool isLoading;

        public ObservableCollection<Article>? NewsList { get; set; } =
            [];

        [RelayCommand]
        void Appearing(CollectionView collectionView) {

            IsLoading = true;

            NewsCollectionView = collectionView;

            LoadNews();
        }
        async void LoadNews() {

            var language = deviceLanguageService.GetDeviceLanguage();

            var newsUrl = AppConstants.GetNewsUrl(language);

            NewsList!.Clear();

            var newsObj = await httpService.GetAsync<News>(newsUrl);

            foreach (var item in newsObj!.Articles!) {
                if (string.IsNullOrWhiteSpace(item.Author)) {
                    item.Author = "Uknown Author";
                }

                NewsList.Add(item);
            }

            IsLoading = false;

            if (scrollToPosition > 0) {

                NewsCollectionView!.ScrollTo(scrollToPosition,
                    -1, ScrollToPosition.Center,
                    false);
            }
        }


        [RelayCommand]
        protected void ShowNewsDetail(Article article) {
            if (article != null) {

                scrollToPosition = NewsList!.IndexOf(article);

                Shell.Current.GoToAsync($"{nameof(ArticleDetailsPage)}",
                    true,
                    new Dictionary<string, object> {

                        { "articleObj", article }
                    });
            }
        }
    }
}