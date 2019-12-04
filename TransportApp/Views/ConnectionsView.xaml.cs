using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            var viewModel = new ConnectionsViewModel();
            DataContext = viewModel;
            viewModel.RequestClose += this.Close;
        }

        /// <summary>
        /// Get if new focus element is ListViewItem.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void LostFocusOnLocation(object sender, KeyboardFocusChangedEventArgs e)
        {
            ConnectionsViewModel.NewFocusElementIsListViewItem = (FrameworkElement) e.NewFocus is ListViewItem;
        }
    }
}
