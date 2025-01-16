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
        tb.UpdateProperties(pageViewModel.ArticleList,
            "Title", "Title", false, OccurrenceMode.None);
        App.WindowInstance!.TitleBar = tb;

        tb.ComboBox.SelectionChanged += ComboBox_SelectionChanged;
    }

    private void ComboBox_SelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (sender is SfComboBox combobox)
        {
            NewsList.ScrollTo(combobox.SelectedIndex, -1, ScrollToPosition.Center);
        }

    }
}