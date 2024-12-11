namespace METROWIND.Interfaces
{
    public interface IHttpService
    {
        Task<T?> GetAsync<T>(string url);
    }
}
