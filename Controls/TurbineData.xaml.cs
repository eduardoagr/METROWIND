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


    public static readonly BindableProperty Co2KgRemovedProperty = BindableProperty.Create(
        nameof(Co2KgRemoved), typeof(string), typeof(TurbineData), null, BindingMode.TwoWay);

    public string Co2KgRemoved {
        get => (string)GetValue(Co2KgRemovedProperty);
        set => SetValue(Co2KgRemovedProperty, value);
    }


    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
        nameof(TapCommand), typeof(ICommand), typeof(TurbineData));

    public ICommand TapCommand {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }

    public static readonly BindableProperty TapCommandParameterProperty = BindableProperty.Create(
           nameof(TapCommandParameter), typeof(object), typeof(TurbineData));

    public object TapCommandParameter {
        get => GetValue(TapCommandParameterProperty);
        set => SetValue(TapCommandParameterProperty, value);
    }
}