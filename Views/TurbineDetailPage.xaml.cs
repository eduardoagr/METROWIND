namespace METROWIND.Views;

public partial class TurbineDetailPage: ContentPage
{
    int ImagesLoaded = 0;

    public TurbineDetailPage(TurbineDetailPageViewModel turbineDetailVViewModel)
    {
        InitializeComponent();
        BindingContext = turbineDetailVViewModel;

    }

    private void CurrentImage_Loaded(object sender, EventArgs e)
    {
        ImagesLoaded++;

        if (BindingContext is TurbineDetailPageViewModel viewModel)
        {
            var totalImages = viewModel.SelectedTurbine!.Turbine!.ImagesURLs!.Count;

            if (ImagesLoaded == totalImages)
            {
                viewModel.AreImagesLoaded = true;
            }
        }
    }
}