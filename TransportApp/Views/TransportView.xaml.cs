namespace TransportApp.Views
{
    using ViewModels;

    /// <summary>
    /// Interaction logic for TransportView.xaml
    /// </summary>
    public partial class TransportView
    {
        public TransportView()
        {
            InitializeComponent();
            var viewModel = new TransportViewModel();
            DataContext = viewModel;
            viewModel.RequestClose += this.Close;
        }
    }
}
