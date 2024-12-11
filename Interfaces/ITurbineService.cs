namespace METROWIND.Interfaces
{
    public interface ITurbineService
    {
        ObservableCollection<TurbinePin> TurbinePins { get; }

        ICommand PinClickedCommand { get; }

        Task InitializeAsync();

        void SetPinClickHandler(IPinClickHandler pinClickHandler);
    }
}
