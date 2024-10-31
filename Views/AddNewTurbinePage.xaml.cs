namespace METROWIND.Views;

public partial class AddNewTurbinePage : ContentPage {
    public AddNewTurbinePage(AddNewTurbinePageViewModel addNewTurbinePageViewModel) {
        InitializeComponent();
        BindingContext = addNewTurbinePageViewModel;
    }

    protected override bool OnBackButtonPressed() {
        return true;
    }
}