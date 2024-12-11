namespace METROWIND.Interfaces
{
    public interface IFirestoreService
    {
        Task<bool> InitializeFirestoreAsync();

        FirestoreDb GetFirestoreDb();
    }
}
