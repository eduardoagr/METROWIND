//namespace METROWIND.Views.PopUps;

//public partial class AddTurbnePopUp : Popup {

//    public AddTurbnePopUp(AddTurbnePopUpViewModel addTurbnePopUpViewModel) {
//        InitializeComponent();
//        BindingContext = addTurbnePopUpViewModel;
//    }

//    private async void PopUpBorder_Loaded(object sender, EventArgs e) {

//        PopUpBorder.Scale = 0.5;
//        PopUpBorder.Opacity = 0;

//        await Task.WhenAll(new Task[]
//        {
//             PopUpBorder.ScaleTo(1, 500, Easing.CubicInOut),
//             PopUpBorder.FadeTo(1, 500, Easing.CubicInOut)
//        });

//    }
//}