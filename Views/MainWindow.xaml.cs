namespace METROWIND.Views;

public partial class MainWindow: Window
{
    public MainWindow(AppShell shell)
    {
        InitializeComponent();

        Page = shell;
    }
}