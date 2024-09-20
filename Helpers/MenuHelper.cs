using METROWIND.Resources;

namespace METROWIND.Helpers {

    public static class MenuHelper {

        public static void ConfigureFlyoutMenu(Shell shell) {
            // Define the items to add to the flyout menu
            var flyoutItems = new List<(string Title, string Icon, Type ContentType, string Route)>
            {
                (AppResource.HomePage, "home.png", typeof(HomePage), "HomePage"),
                (AppResource.Stations, "stations.png", typeof(ChargingStationsMapPage), "ChargingStationsMapPage")
            };

            // Add each flyout item to the Shell
            foreach (var (title, icon, contentType, route) in flyoutItems) {
                var flyoutItem = CreateFlyoutItem(title, icon, contentType, route);
                shell.Items.Add(flyoutItem);
            }
        }

        public static FlyoutItem CreateFlyoutItem(string title, string icon, Type contentType, string route) {
            return new FlyoutItem {
                Title = title,
                Icon = icon,
                Items =
                {
                    new ShellContent
                    {
                        ContentTemplate = new DataTemplate(contentType),
                        Route = route,
                    }
                }
            };
        }
    }
}
