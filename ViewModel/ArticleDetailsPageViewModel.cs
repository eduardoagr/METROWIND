namespace METROWIND.ViewModel {

    [QueryProperty(nameof(ArticleObj), "articleObj")]
    public partial class ArticleDetailsPageViewModel : ObservableObject {

        [ObservableProperty]
        Article? articleObj;

    }
}
