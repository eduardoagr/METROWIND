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
            "Article.Title", "Article.Title");
        App.WindowInstance!.TitleBar = tb;

        tb.ComboBox.SelectionChanged += ComboBox_SelectionChanged;
    }

    private void ComboBox_SelectionChanged(object? sender, Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        var combobox = sender as SfComboBox;

        Debug.WriteLine($"Selected item: {combobox!.SelectedItem}, Index: {combobox.SelectedIndex}");

    }
}