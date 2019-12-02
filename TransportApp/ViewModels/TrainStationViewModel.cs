using System;
using System.Windows.Input;
using TransportApp.Base;
using TransportApp.Views;

namespace TransportApp.ViewModels
{
    public class TrainStationViewModel : BaseViewModel
    {
        #region Initialization

        public TrainStationViewModel()
        {

        }

        #endregion

        #region Commands

        #region Show connections

        public ICommand ShowConnectionsCommand =>_showConnectionsCommand ?? (_showConnectionsCommand = new RelayCommand(OnExecuteShowConnections));
        private ICommand _showConnectionsCommand;

        private void OnExecuteShowConnections()
        {
            var connectionsView = new ConnectionsView();
            RequestClose?.Invoke();
            connectionsView.ShowDialog();
        }

        #endregion

        #region Show station board

        public ICommand ShowStationBoardCommand =>_showStationBoardCommand ?? (_showStationBoardCommand = new RelayCommand(OnExecuteShowStationBoard));
        private ICommand _showStationBoardCommand;

        private void OnExecuteShowStationBoard()
        {
            var stationBoardView = new StationBoardView();
            RequestClose?.Invoke();
            stationBoardView.ShowDialog();
        }

        #endregion

        #region Show train station

        public ICommand ShowTrainStation =>_showTrainStation ?? (_showTrainStation = new RelayCommand(OnExecuteShowTrainStation));
        private ICommand _showTrainStation;

        private void OnExecuteShowTrainStation()
        {
            var trainStationView = new TrainStationView();
            RequestClose?.Invoke();
            trainStationView.ShowDialog();
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the current location.
        /// </summary>
        public string CurrentLocation
        {
            get => _currentLocation ?? (_currentLocation = "Luzern");
            set
            {
                _currentLocation = value;
                RaisePropertyChanged();
            }
        }
        private string _currentLocation;

        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods



        #endregion
    }
}
