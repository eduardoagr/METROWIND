
namespace METROWIND.ViewModel {
    public partial class HomePageViewModel(HttpService httpService) : ObservableObject {

        [ObservableProperty]
        bool isLoading;

        public ObservableCollection<Article>? NewsList { get; set; } =
            [];

        [RelayCommand]
        void Appearing() {
            IsLoading = true;
            LoadNews();
        }
        async void LoadNews() {

            NewsList!.Clear();

            var newsObj = await httpService.GetAsync<News>(Apis.NEWS_URL);

            foreach (var item in newsObj!.articles!) {
                if (string.IsNullOrWhiteSpace(item.author)) {
                    item.author = "Uknown Author";
                }

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

        [RelayCommand]
        void OpenShareMenu(string NewsUrl) {

            Share.Default.RequestAsync(new ShareTextRequest {
                Uri = NewsUrl,
                Title = "Check out this article"
            });
        }
    }
}