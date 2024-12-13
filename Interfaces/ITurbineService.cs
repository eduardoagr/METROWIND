namespace METROWIND.Interfaces
{
    public interface ITurbineService
    {
        event Action NoInternet;

        ObservableCollection<TurbinePin> TurbinePins { get; }

        ICommand PinClickedCommand { get; }

        Task InitializeAsync();

        void SetPinClickHandler(IPinClickHandler pinClickHandler);
    }
}
