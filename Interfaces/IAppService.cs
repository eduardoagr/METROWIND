namespace METROWIND.Interfaces
{
    public interface IAppService
    {
        Task NavigateToPage(string pageName);

        Task NavigateBack(string quary = "..");

        Task NavigateToPage(string pageName, Dictionary<string, object> objectToPass, bool animate = true);

        Task ShowAlert(string title, string message, string confirm);
    }
}
