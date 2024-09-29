namespace METROWIND.Models {

    //This is a Temporary class, jus to test see just shit\

    public class TurbineDataMessage(string? turbinaName, string? turbneAddres, string? turbineFormatedDate, Location turbineLocation) {

        public string? TurbineName { get; } = turbinaName;

        public string? TurbneAddres { get; } = turbneAddres;

        public string? TurbineFormatedDate { get; } = turbineFormatedDate;

        public Location TurbineLocation { get; set; } = turbineLocation;
    }
}
