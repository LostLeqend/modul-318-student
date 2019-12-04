using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        /// <summary>
        /// Get if new focus element is ListViewItem.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void LostFocusOnLocation(object sender, KeyboardFocusChangedEventArgs e)
        {
            TransportViewModel.NewFocusElementIsListViewItem = (FrameworkElement) e.NewFocus is ListViewItem;
        }
    }
}
