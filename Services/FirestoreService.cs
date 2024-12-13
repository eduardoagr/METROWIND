using Google.Apis.Auth.OAuth2;

namespace METROWIND.Services;

public class FirestoreService(ILogger<FirestoreService> logger): IFirestoreService
{

    private FirestoreDb? _firestoreDb;

    public async Task<bool> InitializeFirestoreAsync()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync
                ("metrowind_firestore_config.json");
            using var reader = new StreamReader(stream);
            string json = await reader.ReadToEndAsync();
            var credential = GoogleCredential.FromJson(json);
            var builder = new FirestoreDbBuilder
            {
                ProjectId = AppConstants.FIREBASE_PROJECT_ID,
                ChannelCredentials = credential.ToChannelCredentials()
            };
            _firestoreDb = builder.Build();

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError("Failed to initialize Firestore: {Message}", ex.Message);
            return false;
        }
    }

    public FirestoreDb GetFirestoreDb() => _firestoreDb!;
}
