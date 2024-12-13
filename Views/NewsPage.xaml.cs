using SelectionChangedEventArgs = Syncfusion.Maui.Inputs.SelectionChangedEventArgs;

namespace METROWIND.Views;

public partial class NewsPage: ContentPage
{
    private readonly NewsPageViewModel pageViewModel;

    public NewsPage(NewsPageViewModel newsPageViewModel)
    {
        InitializeComponent();
        pageViewModel = newsPageViewModel;
        BindingContext = newsPageViewModel;

        var tb = new AppTitleBar();
        tb.SetItemSource(pageViewModel.ArticleList,
            "Title", "Title");
        App.WindowInstance!.TitleBar = tb;

        tb.ComboBox.SelectionChanged += ComboBox_SelectionChanged;
    }

    private void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        var combobox = sender as SfComboBox;

        Debug.WriteLine($"Selected item: {combobox!.SelectedItem}, Index: {combobox.SelectedIndex}");

    }
}