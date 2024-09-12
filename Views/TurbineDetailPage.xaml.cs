namespace METROWIND.Views;

public partial class TurbineDetailPage : ContentPage {
    public TurbineDetailPage(TurbineDetailPageViewModel turbineDetailVViewModel) {
        InitializeComponent();
        BindingContext = turbineDetailVViewModel;
    }
}