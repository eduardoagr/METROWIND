using Google.Apis.Auth.OAuth2;
using Google.Cloud.Firestore;

using Grpc.Auth;

namespace METROWIND.Services {
    public class FirestoreService {

        private FirestoreDb? _firestoreDb;

        public async Task<bool> InitializeFirestoreAsync() {


            try {
                using var stream = await FileSystem.OpenAppPackageFileAsync("metrowind_firestore_config.json");
                using var reader = new StreamReader(stream);
                string json = await reader.ReadToEndAsync();
                var credential = GoogleCredential.FromJson(json);
                var builder = new FirestoreDbBuilder {
                    ProjectId = "firestoremauidemo-70329",
                    ChannelCredentials = credential.ToChannelCredentials()
                };
                _firestoreDb = builder.Build();

                return true;
            }
            catch (Exception ex) {
                Debug.WriteLine($"Failed to initialize Firestore: {ex.Message}");
                return false;
            }
        }

        public FirestoreDb GetFirestoreDb() => _firestoreDb!;
    }
}
