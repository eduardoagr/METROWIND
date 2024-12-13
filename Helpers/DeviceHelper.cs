namespace METROWIND.Helpers
{
    public static class DeviceHelper
    {
        public static void AddOrRemoveContentBasedOnDevice(View content)
        {
            Layout parentLayout = FindParentLayout(content)!;

            if (DeviceInfo.Idiom == DeviceIdiom.Phone)
            {
                if (content.Parent == null)
                {
                    parentLayout!.Children.Add(content);
                }
            }
            else
            {
                if (content.Parent != null)
                {
                    parentLayout!.Children.Remove(content);
                }
            }
        }

        private static Layout? FindParentLayout(View view)
        {
            // Return null immediately if the view is null
            if (view == null)
            {
                return null;
            }

            // Start traversing the parent chain
            var parent = view.Parent;

            // Traverse up the visual tree until we find a Layout or reach the root
            while (parent != null)
            {
                if (parent is Layout layout)
                {
                    return layout; // Return the first Layout parent found
                }

                parent = parent.Parent;
            }

            return null; // No Layout found, return null
        }
    }
}
