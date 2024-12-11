namespace METROWIND.Converters
{
    public class ImgSourceConverter: IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            var source = value as string;
            if (source != null)
            {
                var res = GetHttpImage(source);
                if (res)
                {
                    return true;
                }
            }
            return "no_image.png";
        }
        private bool GetHttpImage(string source)
        {
            var client = new HttpClient();
            try
            {
                var response = client.GetAsync(source).Result;
                if (response != null && response.IsSuccessStatusCode)
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return true;
        }
    }
}
