namespace METROWIND.Views;

public partial class HomePage : ContentPage {
    public HomePage(HomePageViewModel homePageViewModel) {
        InitializeComponent();
        BindingContext = homePageViewModel;
    }
}