using METROWIND.Helpers;

namespace METROWIND.Views;

public partial class TurbinesCollectionPage: ContentPage
{
    public TurbinesCollectionPageViewModel PageViewModel { get; }

    public TurbinesCollectionPage(TurbinesCollectionPageViewModel
        turbinesCollectionPageViewModel)
    {
        InitializeComponent();

        PageViewModel = turbinesCollectionPageViewModel;

        PageViewModel.ColletionComboBox = combobox;
        PageViewModel.TurbinesCollection = TurbineCollection;

        BindingContext = turbinesCollectionPageViewModel;

        var tb = new AppTitleBar();
        tb.SetItemSource(PageViewModel.TurbinePins,
            "Turbine.Name", "Turbine.Name");
        App.WindowInstance!.TitleBar = tb;
        PageViewModel.ColletionComboBox = tb.ComboBox;
        tb.ComboBox.SelectionChanged += ComboBox_SelectionChanged;


        DeviceHelper.AddOrRemoveContentBasedOnDevice(MobileContent);

    }

    private void ComboBox_SelectionChanged(object? sender,
        Syncfusion.Maui.Inputs.SelectionChangedEventArgs e)
    {
        PageViewModel.SelectedItemChangeCommand.Execute(null);

        combobox.SelectedValue = string.Empty;
    }
}