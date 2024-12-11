namespace METROWIND.ViewModel
{

    [QueryProperty(nameof(ArticleObj), "articleObj")]
    public partial class ArticleDetailsPageViewModel(IShare share): ObservableObject
    {
        [ObservableProperty]
        Article? articleObj;

        [RelayCommand]
        async Task OpenShareMenu(Article article)
        {
            await share.RequestAsync(new ShareTextRequest
            {
                Uri = article.Url,
                Title = "Check out this article"
            });
        }

    }
}
