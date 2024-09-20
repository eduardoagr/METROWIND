namespace METROWIND.ViewModel {

    [QueryProperty(nameof(ArticleObj), "articleObj")]
    public partial class ArticleDetailsPageViewModel : ObservableObject {

        [ObservableProperty]
        Article? articleObj;

        [RelayCommand]
        private static void OpenShareMenu(Article article) {

            Share.Default.RequestAsync(new ShareTextRequest {
                Uri = article.Url,
                Title = "Check out this article"
            });
        }

    }
}
