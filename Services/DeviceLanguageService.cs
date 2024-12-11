namespace METROWIND.Services
{
    public class DeviceLanguageService: IDeviceLanguageService
    {
        public CultureInfo GetDeviceCultureInfo()
        {
            return CultureInfo.CurrentCulture;
        }
    }
}
