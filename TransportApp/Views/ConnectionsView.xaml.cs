namespace TransportApp.Views
{
    using ViewModels;

    /// <summary>
    /// Interaction logic for ConnectionsView.xaml
    /// </summary>
    public partial class ConnectionsView
    {
        public ConnectionsView()
        {
            InitializeComponent();
            DataContext = new ConnectionsViewModel();
        }
    }
}
