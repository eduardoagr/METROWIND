namespace METROWIND.Controls;

public partial class TurbineData : ContentView {
    public TurbineData() {
        InitializeComponent();
    }


    public static readonly BindableProperty TurbineNameProperty = BindableProperty.Create(
        nameof(TurbineName), typeof(string), typeof(TurbineData));

    public string TurbineName {
        get => (string)GetValue(TurbineNameProperty);
        set => SetValue(TurbineNameProperty, value);
    }


    public static readonly BindableProperty TurbineAddresProperty = BindableProperty.Create(
        nameof(TurbineAddres), typeof(string), typeof(TurbineData));

    public string TurbineAddres {
        get => (string)GetValue(TurbineAddresProperty);
        set => SetValue(TurbineAddresProperty, value);
    }


    public static readonly BindableProperty TurbineCreationDateProperty = BindableProperty.Create(
        nameof(TurbineCreationDate), typeof(string), typeof(TurbineData));

    public string TurbineCreationDate {
        get => (string)GetValue(TurbineCreationDateProperty);
        set => SetValue(TurbineCreationDateProperty, value);
    }

}