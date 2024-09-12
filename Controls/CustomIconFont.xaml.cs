namespace METROWIND.Controls;

public partial class CustomIconFont : ContentView {
    public CustomIconFont() {
        InitializeComponent();
    }


    public static readonly BindableProperty TextIconProperty = BindableProperty.Create(
        nameof(TextIcon), typeof(string),
        typeof(CustomIconFont));

    public string TextIcon {
        get => (string)GetValue(TextIconProperty);
        set => SetValue(TextIconProperty, value);
    }



    public static readonly BindableProperty FontFamilyProperty = BindableProperty.Create(
        nameof(FontFamily), typeof(string),
        typeof(CustomIconFont));

    public string FontFamily {
        get => (string)GetValue(FontFamilyProperty);
        set => SetValue(FontFamilyProperty, value);
    }


    public static readonly BindableProperty TextIconColorProperty = BindableProperty.Create(
        nameof(TextIconColor), typeof(Color),
        typeof(CustomIconFont));

    public Color TextIconColor {
        get => (Color)GetValue(TextIconColorProperty);
        set => SetValue(TextIconColorProperty, value);
    }


    public static readonly BindableProperty CaptionProperty = BindableProperty.Create(
        nameof(Caption), typeof(string),
        typeof(CustomIconFont));

    public string Caption {
        get => (string)GetValue(CaptionProperty);
        set => SetValue(CaptionProperty, value);
    }

}