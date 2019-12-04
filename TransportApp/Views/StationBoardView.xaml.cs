using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
            var viewModel = new StationBoardViewModel();
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
            StationBoardViewModel.NewFocusElementIsListViewItem = (FrameworkElement) e.NewFocus is ListViewItem;
        }
    }
}
