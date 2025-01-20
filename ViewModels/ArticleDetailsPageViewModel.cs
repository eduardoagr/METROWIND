namespace METROWIND.ViewModel
{

    [QueryProperty(nameof(Url), "articleURL")]
    public partial class ArticleDetailsPageViewModel(IShare share): ObservableObject
    {
        [ObservableProperty]
        string? url;

        [RelayCommand]
        async Task OpenShareMenu()
        {
            await share.RequestAsync(new ShareTextRequest
            {
                Uri = url,
                Title = "Check out this article"
            });
        }

    }
}
