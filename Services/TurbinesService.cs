namespace METROWIND.Services
{
    public class TurbinesService
    {
        private const string collectionName = "turbines";
        private readonly FirestoreService _firestoreService;
        private readonly BlobServiceClient _blobServiceClient;
        private static System.Timers.Timer? _timer;
        private FirestoreDb? _firestoreDb;

        public ObservableCollection<TurbinePin> TurbinePins { get; set; } = [];

        public TurbinesService(FirestoreService firestoreService, BlobServiceClient blobServiceClient)
        {
            _firestoreService = firestoreService;
            _blobServiceClient = blobServiceClient;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await _firestoreService.InitializeFirestoreAsync();
            _firestoreDb = _firestoreService.GetFirestoreDb();
            if (_firestoreDb != null)
            {
                await LoadOrInitializeTurbineAsync();
                //InitializeTimer();
            }
        }

        async Task LoadOrInitializeTurbineAsync()
        {
            var turbinesRef = _firestoreDb!.Collection(collectionName);
            var snapshot = await turbinesRef.GetSnapshotAsync();

            if (snapshot.Count == 0)
            {
                var turbine = new Turbine
                {
                    Id = "EC-G-SB",
                    Country = "Ecuador",
                    Name = "Estación Ciudadela Simón Bolívar",
                    Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                    Latitude = -2.151993,
                    Longitude = -79.886109,
                    InstalationDateTime = new DateTime(
                        2024, 8, 2, 0, 0, 0, DateTimeKind.Utc),
                    ImagesURLs = [],
                };

                turbine.RemovedCo2Kilograms = Math.Round(
                    turbine.EnergyProduced * turbine.Co2EmissionOffset, 5);

                await AddTurbineImages(turbine);

                var turbineDocRef = turbinesRef.Document(turbine.Id);

                await turbineDocRef.SetAsync(turbine);

                AddToObservable(turbine);
            }
            else
            {
                foreach (var document in snapshot.Documents)
                {
                    var turbine = document.ConvertTo<Turbine>();
                    turbine.Id = document.Id;
                    await AddTurbineImages(turbine);
                    AddToObservable(turbine);
                }
            }
        }

        private async Task AddTurbineImages(Turbine turbine)
        {
            turbine.ImagesURLs!.Clear();

            var containerClient = _blobServiceClient.GetBlobContainerClient(turbine.Country!.ToLower());
            await foreach (var item in containerClient.GetBlobsAsync())
            {
                var blobClient = containerClient.GetBlobClient(item.Name);
                turbine.ImagesURLs!.Add(blobClient.Uri.ToString());
            }
        }

        private void AddToObservable(Turbine turbine)
        {
            TurbinePins.Add(new TurbinePin { Turbine = turbine });
        }

        public ObservableCollection<TurbinePin> GetTurbinePinsForUI(ICommand pinClickedCommand)
        {
            foreach (var pin in TurbinePins.OrderBy(t => t.Turbine?.InstalationDateTime))
            {
                pin.PinClickedCommand = pinClickedCommand;
            }
            return TurbinePins;
        }

        private void InitializeTimer()
        {
            _timer = new System.Timers.Timer(1000); // 1000 milliseconds = 1 second
            _timer.Elapsed += async (sender, e) => await UpdateCO2ValueAsync();
            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        private async Task UpdateCO2ValueAsync()
        {
            var turbineRef = _firestoreDb!.Collection(collectionName).Document("EC-G-SB");
            var snapshot = await turbineRef.GetSnapshotAsync();
            var turbine = snapshot.ConvertTo<Turbine>();

            var beforeUpdate = turbine.RemovedCo2Kilograms;

            turbine.FinalCo2Removed = beforeUpdate;

            turbine.RemovedCo2Kilograms = Math.Round(beforeUpdate + 0.0007, 5);

            await turbineRef.SetAsync(turbine, SetOptions.Overwrite);

            UpdateTurbineInCollection(turbine);
        }

        private void UpdateTurbineInCollection(Turbine updatedTurbine)
        {
            var existingTurbinePin = TurbinePins.FirstOrDefault(
                tp => tp.Turbine!.Id == updatedTurbine.Id);

            if (existingTurbinePin != null)
            {
                existingTurbinePin.Turbine!.RemovedCo2Kilograms = updatedTurbine.RemovedCo2Kilograms;

                existingTurbinePin.Turbine!.FinalCo2Removed = updatedTurbine.RemovedCo2Kilograms;
            }
        }
    }
}
