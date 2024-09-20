namespace METROWIND.Models {

    public class PinModel {

        public string? Label { get; set; }

        public string? Address { get; set; }

        public Location? Location { get; set; }

        public ICommand? MarkerClickedCommand { get; set; }

        public ObservableCollection<string>? Images { get; set; }

        public ObservableCollection<TrbineData>? DataCollection { get; set; }

    }
}