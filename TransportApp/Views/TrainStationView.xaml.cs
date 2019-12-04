using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using SwissTransport;
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
            Map.Center = new Location(46, 8);
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

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var transport = new Transport();
            var station = transport.GetStations(TxtStation.Text).StationList.FirstOrDefault(x => string.Equals(x.Name, TxtStation.Text, StringComparison.CurrentCultureIgnoreCase));
            if (station != null)
            {
                Map.Center = new Location(station.Coordinate.XCoordinate, station.Coordinate.YCoordinate);
                Map.ZoomLevel = 17;
                Map.Mode = new AerialMode();
                TxtStation.Text = string.Empty;
            }
            else
            {
                MessageBox.Show("Station can not be found. Please check if the name is correct.");
                Map.Center = new Location(46,8);
                Map.ZoomLevel = 7;
                Map.Mode = new RoadMode();
            }
               
        }
    }
}
