using TransportApp.ViewModels;

namespace TransportApp.Views
{
    /// <summary>
    /// Interaction logic for TrainStationView.xaml
    /// </summary>
    public partial class TrainStationView
    {
        public TrainStationView()
        {
            InitializeComponent();
            var viewModel = new TrainStationViewModel();
            DataContext = viewModel;
            viewModel.RequestClose += this.Close;
        }
    }
}
