using Timer = System.Timers.Timer;

namespace METROWIND.Services
{
    public class TurbinesService: ITurbineService, ICommandHandler
    {
        private const string collectionName = "turbines";
        private readonly IFirestoreService firestoreService;
        private readonly IBlobService blobService;
        private static Timer? _timer;
        private FirestoreDb? _firestoreDb;
        private IPinClickHandler? _pinClickHandler;

        public ICommand PinClickedCommand { get; private set; }

        public ObservableCollection<TurbinePin> TurbinePins { get; set; } = [];

        public TurbinesService(IFirestoreService firestoreService, IBlobService blobService)
        {
            this.firestoreService = firestoreService;
            this.blobService = blobService;

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
            TurbinePins.Clear();

            bool isInitialized = await firestoreService.InitializeFirestoreAsync();
            if (isInitialized)
            {
                _firestoreDb = firestoreService.GetFirestoreDb();
                if (_firestoreDb != null)
                {
                    await LoadOrInitializeTurbineAsync();
                    //InitializeTimer();
                }
            }
        }

        private async Task LoadOrInitializeTurbineAsync()
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

        private async Task AddTurbineImagesAsync(Turbine turbine)
        {
            turbine.ImagesURLs = (await blobService.GetImagessFromBlob(turbine.Country!)).ToList();
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