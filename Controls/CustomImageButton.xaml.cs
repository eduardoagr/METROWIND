namespace METROWIND.Controls;

public partial class CustomImageButton : ContentView {
    public CustomImageButton() {
        InitializeComponent();
    }


    public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(
        nameof(ImageName), typeof(ImageSource), typeof(CustomImageButton));

    public ImageSource ImageName {
        get => (ImageSource)GetValue(ImageNameProperty);
        set => SetValue(ImageNameProperty, value);
    }


    public static readonly BindableProperty CaptionProperty = BindableProperty.Create(
        nameof(Caption), typeof(string), typeof(CustomImageButton));

    public string Caption {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }


    public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
        nameof(ClickCommand), typeof(ICommand), typeof(CustomImageButton));

    public ICommand ClickCommand {
        get => (ICommand)GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }


    public static readonly BindableProperty ParametersProperty = BindableProperty.Create(
        nameof(Parameters), typeof(int), typeof(CustomImageButton));

    public int Parameters {
        get => (int)GetValue(ParametersProperty);
        set => SetValue(ParametersProperty, value);
    }


}