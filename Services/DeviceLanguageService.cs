namespace METROWIND.Services {
    public class DeviceLanguageService {

        public string GetDeviceLanguage() {

            var culture = CultureInfo.CurrentCulture;

            return culture.TwoLetterISOLanguageName;
        }

    }
}
