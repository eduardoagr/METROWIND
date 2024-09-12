namespace METROWIND.Views;

public partial class ArticleDetailsPage : ContentPage {
    public ArticleDetailsPage(ArticleDetailsPageViewModel articleDetailsPageViewModel) {
        InitializeComponent();
        BindingContext = articleDetailsPageViewModel;
    }
}