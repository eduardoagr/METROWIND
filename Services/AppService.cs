
namespace METROWIND.Services
{
    public class AppService: IAppService
    {
        public async Task NavigateBack(string quary = "..")
        {
            await Shell.Current.GoToAsync(quary);
        }

        public async Task NavigateToPage(string pageName)
        {
            await Shell.Current.GoToAsync(pageName);
        }

        public async Task NavigateToPage(string pageName, Dictionary<string, object> objectToPass, bool animate = true)
        {
            await Shell.Current.GoToAsync(pageName, animate, objectToPass);
        }

        public async Task ShowAlert(string title, string message, string confirm = "OK")
        {
            await Shell.Current.DisplayAlert(title, message, confirm);
        }
    }
}
