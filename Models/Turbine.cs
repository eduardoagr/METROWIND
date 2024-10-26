using METROWIND.Resources;

namespace METROWIND.Models {

    public partial class Turbine : ObservableObject {

        [ObservableProperty]
        int username;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        string? name;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        string? address;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        double? power;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        double? co2EmissionOffset;


        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsValid))]
        double? capacityFator;

        [ObservableProperty]
        Location? location;

        [ObservableProperty]
        DateTime? instalationDateTime;

        [ObservableProperty]
        ObservableCollection<string>? images = [];

        [ObservableProperty]
        string? stringifyInstalationDate = AppResource.TurbineInstalationDateTime;

        string? Label => Name;

        public bool IsValid => Power != 0.0
                             && CapacityFator != 0.0
                             && Co2EmissionOffset != 0.0
                             && !string.IsNullOrEmpty(Name)
                             && !string.IsNullOrEmpty(Address)
                             && !string.IsNullOrEmpty(StringifyInstalationDate);
    }
}