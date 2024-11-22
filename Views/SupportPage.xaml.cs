namespace METROWIND.Views;

public partial class SupportPage : ContentPage
{
    public SupportPage(SupportPageViewModel supportPageViewModel)
    {
        InitializeComponent();
        BindingContext = supportPageViewModel;
    }
}