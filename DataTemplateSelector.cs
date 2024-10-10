namespace METROWIND;

public class DeviceDataTemplateSelector : DataTemplateSelector {

    public required DataTemplate PhoneTemplate { get; set; }

    public required DataTemplate DeskTopTemplate { get; set; }

    protected override DataTemplate OnSelectTemplate(object item, BindableObject container) {
        return (DeviceInfo.Current.Idiom == DeviceIdiom.Desktop) ? DeskTopTemplate : PhoneTemplate;
    }

}
