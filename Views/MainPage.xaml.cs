using METROWIND.ViewModel;

namespace METROWIND.Views {
    public partial class MainPage : ContentPage {

        public MainPage(MainPageViewModel mainPageViewModel) {
            InitializeComponent();
            BindingContext = mainPageViewModel;
        }

    }
}
