using System.Windows.Input;
using TransportApp.Base;
using TransportApp.Views;

namespace TransportApp.ViewModels
{
    public class ConnectionsViewModel
    {
        #region Initialization



        #endregion

        #region Commands

        #region Show connections

        public ICommand ShowConnectionsCommand => _showConnectionsCommand ?? (_showConnectionsCommand = new RelayCommand(OnExecuteShowConnections));
        private ICommand _showConnectionsCommand;

        private void OnExecuteShowConnections()
        {
            var connectionsView = new ConnectionsView();
            connectionsView.ShowDialog();
        }

        #endregion

        #region Show station board

        public ICommand ShowStationBoardCommand => _showStationBoardCommand ?? (_showStationBoardCommand = new RelayCommand(OnExecuteShowStationBoard));
        private ICommand _showStationBoardCommand;

        private void OnExecuteShowStationBoard()
        {
            var stationBoardView = new StationBoardView();
            stationBoardView.ShowDialog();
        }

        #endregion

        #region Show train station

        public ICommand ShowTrainStation => _showTrainStation ?? (_showTrainStation = new RelayCommand(OnExecuteShowTrainStation));
        private ICommand _showTrainStation;

        private void OnExecuteShowTrainStation()
        {
            var trainStationView = new TrainStationView();
            trainStationView.ShowDialog();
        }

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Methods



        #endregion
    }
}
