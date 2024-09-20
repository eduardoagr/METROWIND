namespace METROWIND.Controls;

public partial class CustomImage : ContentView {
    public CustomImage() {
        InitializeComponent();
    }


    public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(
        nameof(ImageName), typeof(ImageSource), typeof(CustomImage));

    public ImageSource ImageName {
        get => (ImageSource)GetValue(ImageNameProperty);
        set => SetValue(ImageNameProperty, value);
    }


    public static readonly BindableProperty CaptionProperty = BindableProperty.Create(
        nameof(Caption), typeof(string), typeof(CustomImage));

    public string Caption {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }


    public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
        nameof(ClickCommand), typeof(ICommand), typeof(CustomImage));

    public ICommand ClickCommand {
        get => (ICommand)GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }


    public static readonly BindableProperty ParametersProperty = BindableProperty.Create(
        nameof(Parameters), typeof(int), typeof(CustomImage));

    public int Parameters {
        get => (int)GetValue(ParametersProperty);
        set => SetValue(ParametersProperty, value);
    }


}