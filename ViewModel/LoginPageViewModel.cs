using METROWIND.Resources;

using User = METROWIND.Models.User;

namespace METROWIND.ViewModel {

    public partial class LoginPageViewModel : ObservableObject {

        private readonly FirebaseAuthClient client;

        [ObservableProperty]
        bool isHidden = true;

        [ObservableProperty]
        User user = new();

        public LoginPageViewModel(FirebaseAuthClient firebaseAuthClient) {

            client = firebaseAuthClient;

            // Check if client.User and client.User.Uid are not null
            if (client.User != null && !string.IsNullOrEmpty(client.User.Uid)) {

                Shell.Current.GoToAsync($"//{nameof(HomePage)}", true);
            }
        }


        [RelayCommand]
        async Task CreateAccount(SfPopup views) {

            try {
                var account = await client.CreateUserWithEmailAndPasswordAsync(User.Email, User.Password);

                await Shell.Current.DisplayAlert(
                    AppResource.Success, AppResource.AccountCreated, "Ok");

                if (views != null) {
                    views.IsOpen = false;
                }
            }
            catch (FirebaseAuthException ex) {
                string message = ex.Reason switch {
                    AuthErrorReason.InvalidEmailAddress => AppResource.InvalidEmail,
                    AuthErrorReason.UserNotFound => AppResource.NotExist,
                    AuthErrorReason.WeakPassword => AppResource.WeekPassord,
                    AuthErrorReason.EmailExists => AppResource.UserExist,
                    _ => AppResource.Uknown
                };
                await Shell.Current.DisplayAlert("Error", message, "Ok");
            }
        }

        [RelayCommand]
        void RegisterPopUp(SfPopup views) {

            if (views != null) {

                views.IsOpen = true;
            }
        }

        [RelayCommand]
        async Task Login() {
            try {

                await client.SignInWithEmailAndPasswordAsync(User.Email, User.Password);

                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            catch (FirebaseAuthException ex) {
                string message = ex.Reason switch {
                    AuthErrorReason.InvalidEmailAddress => AppResource.InvalidEmail,
                    AuthErrorReason.UserNotFound => AppResource.NotExist,
                    AuthErrorReason.WeakPassword => AppResource.WeekPassord,
                    AuthErrorReason.EmailExists => AppResource.UserExist,
                    _ => AppResource.Uknown
                };
                await Shell.Current.DisplayAlert("Error", message, "Ok");
            }
        }

        [RelayCommand]
        void MakePasswordVisible(Label label) {
            if (IsHidden == true) {
                IsHidden = false;
                label.Text = MaterialFonts.Visibility;
            }
            else {
                IsHidden = true;
                label.Text = MaterialFonts.Visibility_off;
            }
        }
    }
}
