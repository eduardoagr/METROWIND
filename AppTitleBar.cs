namespace METROWIND
{
    public class AppTitleBar: TitleBar
    {
        public SfComboBox ComboBox { get; set; }

        public AppTitleBar()
        {
            ComboBox = CreateComboBox()!;

            Icon = "icon_win.png";
            Title = "METROWIND";
            BackgroundColor = Color.FromArgb("#FF3C155F");
            HeightRequest = 48;
            Content = ComboBox;
        }

        private SfComboBox? CreateComboBox()
        {
            return new SfComboBox
            {
                Margin = new Thickness(5),
                IsEditable = true,
                IsClearButtonVisible = false,
                HighlightedTextColor = Colors.Red,
                HighlightedTextFontAttributes = FontAttributes.Bold,
                IsFilteringEnabled = true,
                TextHighlightMode = OccurrenceMode.MultipleOccurrence,
                NoResultsFoundText = AppResource.NotFound
            };
        }

        public void SetItemSource(
            IEnumerable<object> itemSouce,
            string displayMemberPath,
            string textMemberPath)
        {
            ComboBox!.ItemsSource = itemSouce;
            ComboBox.DisplayMemberPath = displayMemberPath;
            ComboBox.TextMemberPath = textMemberPath;
        }
    }
}
