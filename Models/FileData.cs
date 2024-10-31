namespace METROWIND.Models {
    public class FileData {

        public string? FullPath { get; set; }

        public string? FileName { get; set; }

        public override bool Equals(object? obj) =>
        obj is FileData data &&
        FileName == data.FileName;

        public override int GetHashCode() =>
        FileName?.GetHashCode() ?? 0;
    }
}
