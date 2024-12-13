using Timer = System.Timers.Timer;

namespace METROWIND.Services
{
    public class TurbinesService: ITurbineService, ICommandHandler
    {
        public event Action NoInternet;

        private const string collectionName = AppConstants.COLLECTIONNAME;
        private readonly IFirestoreService _firestoreService;
        private readonly IBlobService _blobService;
        private readonly IConnectivity _connectivity;
        private static Timer? _timer;
        private FirestoreDb? _firestoreDb;
        private IPinClickHandler? _pinClickHandler;
        private bool isInitializing = false;

        public ICommand PinClickedCommand { get; private set; }

        public ObservableCollection<TurbinePin> TurbinePins { get; set; } = [];

        public TurbinesService(IFirestoreService firestoreService, IBlobService blobService, IConnectivity connectivity)
        {
            _firestoreService = firestoreService;
            _blobService = blobService;
            _connectivity = connectivity;
            AssingCommand();

        }

        private void AssingCommand()
        {
            PinClickedCommand = new Command<TurbinePin>(async (pin) =>
            {
                if (_pinClickHandler != null)
                {
                    await _pinClickHandler.PinMarkerClicked(pin);
                }
            });
        }

        public async Task InitializeAsync()
        {
            if (isInitializing)
            {
                return; // Prevent multiple initializations
            }

            isInitializing = true;
            TurbinePins.Clear();
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    NoInternet?.Invoke();
                    isInitializing = false;
                    return;
                }
                bool isInitialized = await _firestoreService.InitializeFirestoreAsync();
                if (isInitialized)
                {
                    _firestoreDb = _firestoreService.GetFirestoreDb();
                    if (_firestoreDb != null)
                    {
                        await LoadOrInitializeTurbineAsync();
                        //InitializeTimer();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Initialization failed: {ex.Message}");
            }
            finally
            {
                isInitializing = false;
            }
        }


        private async Task LoadOrInitializeTurbineAsync()
        {
            var turbinesRef = _firestoreDb!.Collection(collectionName);
            var snapshot = await turbinesRef.GetSnapshotAsync();

            if (snapshot.Count == 0)
            {
                await InitializeAsyncDefaultTurbine(turbinesRef);
            }
            else
            {
                var tasks = snapshot.Documents.Select(
                    async document =>
                    {
                        var turbine = document.ConvertTo<Turbine>();
                        turbine.Id = document.Id;
                        await AddTurbineImagesAsync(turbine);
                        return turbine;
                    });

                var turbines = await Task.WhenAll(tasks);
                foreach (var turbine in turbines)
                {
                    AddToCollection(turbine);
                }
            }
        }

        private async Task InitializeAsyncDefaultTurbine(CollectionReference turbinesRef)
        {
            var turbine = new Turbine
            {
                Id = "EC-G-SB",
                Country = "Ecuador",
                Name = "Estación Ciudadela Simón Bolívar",
                Address = "Av. de las Américas, Guayaquil 090513, Ecuador",
                Latitude = -2.151993,
                Longitude = -79.886109,
                InstalationDateTime = new DateTime(2024, 8, 2, 0, 0, 0,
                    DateTimeKind.Utc),

                ImagesURLs = [],
            };

            turbine.RemovedCo2Kilograms = Math.Round(
                turbine.EnergyProduced * turbine.Co2EmissionOffset, 5);

            await AddTurbineImagesAsync(turbine);

            var turbineDocRef = turbinesRef.Document(
                turbine.Id);

            await turbineDocRef.SetAsync(turbine);

            AddToCollection(turbine);
        }

        private async Task AddTurbineImagesAsync(Turbine turbine)
        {
            turbine.ImagesURLs = (await _blobService.GetImagessFromBlob(turbine.Country!)).ToList();
        }

        private void AddToCollection(Turbine turbine)
        {
            var pin = new TurbinePin
            {
                Turbine = turbine,
                PinClickedCommand = PinClickedCommand
            };
            TurbinePins.Add(pin);
        }

        private void InitializeTimer()
        {
            _timer = new Timer(1000); // 1000 milliseconds = 1 second
            _timer.Elapsed += async (sender, e)
                => await UpdateCO2ValueAsync();

            _timer.AutoReset = true;
            _timer.Enabled = true;
        }

        public async Task UpdateCO2ValueAsync()
        {
            var turbineRef = _firestoreDb!.Collection(
                collectionName).Document("EC-G-SB");

            var snapshot = await turbineRef.GetSnapshotAsync();

            var turbine = snapshot.ConvertTo<Turbine>();

            var beforeUpdate = turbine.RemovedCo2Kilograms;

            turbine.FinalCo2Removed = beforeUpdate;

            turbine.RemovedCo2Kilograms = Math.Round(
                beforeUpdate + 0.0007, 5);

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
                existingTurbinePin.Turbine.FinalCo2Removed = updatedTurbine.RemovedCo2Kilograms;
            }
        }

        public void SetPinClickedCommand(ICommand command)
        {
            PinClickedCommand = command;
            foreach (var pin in TurbinePins)
            {
                pin.PinClickedCommand = command;
            }
        }

        public void SetPinClickHandler(IPinClickHandler pinClickHandler)
        {
            _pinClickHandler = pinClickHandler;
        }
    }
}