

namespace METROWIND.Controls;

public partial class CustomImageButton: ContentView
{
    public CustomImageButton()
    {
        InitializeComponent();
    }


    public static readonly BindableProperty ImageNameProperty = BindableProperty.Create(
        nameof(ImageName), typeof(ImageSource), typeof(CustomImageButton));

    public ImageSource ImageName
    {
        get => (ImageSource)GetValue(ImageNameProperty);
        set => SetValue(ImageNameProperty, value);
    }


    public static readonly BindableProperty CaptionProperty = BindableProperty.Create(
        nameof(Caption), typeof(string), typeof(CustomImageButton));

    public string Caption
    {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }


    public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
        nameof(ClickCommand), typeof(ICommand), typeof(CustomImageButton));

    public ICommand ClickCommand
    {
        get => (ICommand)GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }


    public static readonly BindableProperty ParameterProperty = BindableProperty.Create(
        nameof(Parameter), typeof(object), typeof(CustomImageButton));


    public object Parameter
    {
        get => GetValue(ParameterProperty);
        set => SetValue(ParameterProperty, value);
    }


    public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(
        nameof(BorderColor), typeof(Brush), typeof(CustomImageButton),
        Brush.Black);

    public Brush BorderColor
    {
        get => (Brush)GetValue(BorderColorProperty);
        set => SetValue(BorderColorProperty, value);

    }


    public static readonly BindableProperty MapNumberProperty = BindableProperty.Create(
        nameof(MapNumber), typeof(int), typeof(CustomImageButton));

    public int MapNumber
    {
        get => (int)GetValue(MapNumberProperty);
        set => SetValue(MapNumberProperty, value);
    }

}