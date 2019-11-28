namespace TransportApp.Views
{
    using ViewModels;

    /// <summary>
    /// Interaction logic for StationBoardView.xaml
    /// </summary>
    public partial class StationBoardView
    {
        public StationBoardView()
        {
            InitializeComponent();
            DataContext = new StationBoardViewModel();
        }
    }
}
