namespace METROWIND.Views.PopUps;

public partial class AddTurbnePopUp : Popup {

    public AddTurbnePopUp(AddTurbnePopUpViewModel addTurbnePopUpViewModel) {
        InitializeComponent();
        BindingContext = addTurbnePopUpViewModel;
    }

    private async void PopUp_Loaded(object sender, EventArgs e) {

        PopUp.Scale = 0.5;
        PopUp.Opacity = 0;

        await Task.WhenAll(new Task[]
        {
             PopUp.ScaleTo(1, 500, Easing.CubicInOut), // Adjust the duration and easing as needed
             PopUp.FadeTo(1, 500, Easing.CubicInOut)
        });

    }
}