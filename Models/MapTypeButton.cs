namespace METROWIND.Models
{
    public partial class MapTypeButton: ObservableObject
    {
        public string? Caption { get; set; }

        public string? ImageName { get; set; }

        public object? Parameter { get; set; }

        public int MapNumber { get; set; }

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(SelectedColor))]
        bool selected;

        public Brush SelectedColor
        {
            get
            {
                if (Selected)
                {
                    return new SolidColorBrush(Color.FromArgb("#ffff6347"));
                }
                else
                {
                    return new SolidColorBrush(Colors.Black);
                }
            }
        }

    }
}
