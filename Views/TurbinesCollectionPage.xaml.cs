namespace METROWIND.Views;

public partial class TurbinesCollectionPage : ContentPage {
    public TurbinesCollectionPage(TurbinesCollectionPageViewModel turbinesCollectionPageViewModel) {

        InitializeComponent();

        BindingContext = turbinesCollectionPageViewModel;
    }
}