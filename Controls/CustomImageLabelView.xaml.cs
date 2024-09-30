namespace METROWIND.Controls;

public partial class CustomImageLabelView : ContentView {
    public CustomImageLabelView() {
        InitializeComponent();
    }

    public static readonly BindableProperty ImageSourceProperty = BindableProperty.Create(
           nameof(ImageSource), typeof(ImageSource), typeof(CustomImageLabelView), default(ImageSource));

    public ImageSource ImageSource {
        get => (ImageSource)GetValue(ImageSourceProperty);
        set => SetValue(ImageSourceProperty, value);
    }

    public static readonly BindableProperty LabelTextProperty = BindableProperty.Create(
        nameof(LabelText), typeof(string), typeof(CustomImageLabelView), default(string));

    public string LabelText {
        get => (string)GetValue(LabelTextProperty);
        set => SetValue(LabelTextProperty, value);
    }


    public static readonly BindableProperty ClickCommandProperty = BindableProperty.Create(
        nameof(ClickCommand), typeof(ICommand), typeof(CustomImageLabelView));

    public ICommand ClickCommand {
        get => (ICommand)GetValue(ClickCommandProperty);
        set => SetValue(ClickCommandProperty, value);
    }


    public static readonly BindableProperty ClickParameterProperty = BindableProperty.Create(
        nameof(ClickParameter), typeof(int), typeof(CustomImageLabelView));

    public int ClickParameter {
        get => (int)GetValue(ClickParameterProperty);
        set => SetValue(ClickParameterProperty, value);
    }
}