namespace METROWIND
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
            var parent = view.Parent;

            while (parent != null && parent is not Layout)
            {
                parent = parent.Parent;
            }

            return parent as Layout;
        }
    }
}
