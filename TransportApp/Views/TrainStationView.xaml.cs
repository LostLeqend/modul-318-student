using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
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

        /// <summary>
        /// Get if new focus element is ListViewItem.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="KeyboardFocusChangedEventArgs"/> instance containing the event data.</param>
        private void LostFocusOnLocation(object sender, KeyboardFocusChangedEventArgs e)
        {
            TrainStationViewModel.NewFocusElementIsListViewItem = (FrameworkElement) e.NewFocus is ListViewItem;
        }
    }
}
