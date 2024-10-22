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


    public static readonly BindableProperty PointerEnterCommandProperty = BindableProperty.Create(
        nameof(PointerEnterCommand), typeof(ICommand), typeof(TurbineData));

    public ICommand PointerEnterCommand {
        get => (ICommand)GetValue(PointerEnterCommandProperty);
        set => SetValue(PointerEnterCommandProperty, value);
    }


    public static readonly BindableProperty PointerLeaveCommandProperty = BindableProperty.Create(
        nameof(PointerLeaveCommand), typeof(ICommand), typeof(TurbineData));

    public ICommand PointerLeaveCommand {
        get => (ICommand)GetValue(PointerLeaveCommandProperty);
        set => SetValue(PointerLeaveCommandProperty, value);
    }


    public static readonly BindableProperty IsDeleteVisibleProperty = BindableProperty.Create(
        nameof(IsDeleteVisible), typeof(bool), typeof(TurbineData));

    public bool IsDeleteVisible {
        get => (bool)GetValue(IsDeleteVisibleProperty);
        set => SetValue(IsDeleteVisibleProperty, value);
    }


    public static readonly BindableProperty TapCommandProperty = BindableProperty.Create(
        nameof(TapCommand), typeof(ICommand), typeof(TurbineData));

    public ICommand TapCommand {
        get => (ICommand)GetValue(TapCommandProperty);
        set => SetValue(TapCommandProperty, value);
    }


}