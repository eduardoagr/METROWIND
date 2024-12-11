namespace METROWIND.Interfaces
{
    public interface IBlobService
    {
        Task<ImmutableArray<string>> GetImagessFromBlob(string containerName);
    }
}
