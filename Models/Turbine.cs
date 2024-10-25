namespace METROWIND.Models {

    public partial class Turbine : ObservableObject {

        [ObservableProperty]
        int id;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        string? name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        string? address;

        [ObservableProperty]
        Location? location;

        [ObservableProperty]
        DateTime? instalationDateTime;

        [ObservableProperty]
        List<string>? images;

        [ObservableProperty]
        string? stringifyInstalationDate;

        string? Label => Name;

        public bool IsValid => !string.IsNullOrEmpty(Name) &&
                               !string.IsNullOrEmpty(Address);
    }
}