
namespace METROWIND.Models {
    public partial class User : ObservableObject {

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        string? email;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        string? password;

        public bool IsValid => !string.IsNullOrEmpty(Email)
                            && !string.IsNullOrEmpty(Password);

    }
}
