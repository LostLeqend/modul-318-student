using System;
using System.Linq;
using System.Windows.Input;
using SwissTransport;
using TransportApp.Base;
using TransportApp.Views;

namespace TransportApp.ViewModels
{
    public class TransportViewModel : BaseViewModel
    {
        #region Initialization


        #endregion

        #region Commands

        #region Show connections

        public ICommand ShowConnectionsCommand =>_showConnectionsCommand ?? (_showConnectionsCommand = new RelayCommand(OnExecuteShowConnections));
        private ICommand _showConnectionsCommand;

        private void OnExecuteShowConnections(object parameter)
        {
            var connectionsView = new ConnectionsView();
            RequestClose?.Invoke();
            connectionsView.ShowDialog();
        }

        #endregion

        #region Show station board

        public ICommand ShowStationBoardCommand =>_showStationBoardCommand ?? (_showStationBoardCommand = new RelayCommand(OnExecuteShowStationBoard));
        private ICommand _showStationBoardCommand;

        private void OnExecuteShowStationBoard(object parameter)
        {
            var stationBoardView = new StationBoardView();
            RequestClose?.Invoke();
            stationBoardView.ShowDialog();
        }

        #endregion

        #region Show train station

        public ICommand ShowTrainStation =>_showTrainStation ?? (_showTrainStation = new RelayCommand(OnExecuteShowTrainStation));
        private ICommand _showTrainStation;

        private void OnExecuteShowTrainStation(object parameter)
        {
            var trainStationView = new TrainStationView();
            RequestClose?.Invoke();
            trainStationView.ShowDialog();
        }

        #endregion

        #region Complete StartLocation

        public ICommand CompleteStartLocationCommand => _completeStartLocationCommand ?? (_completeStartLocationCommand = new RelayCommand(OnExecuteCompleteStartLocation));
        private ICommand _completeStartLocationCommand;

        private void OnExecuteCompleteStartLocation(object parameter)
        {
            if (!(parameter is Station station))
                return;

            StartLocation = station.Name;
            StationList = null;
            IsCompleteStartLocationActive = false;
        }

        #endregion

        #region Complete EndLocation

        public ICommand CompleteEndLocationCommand => _completeEndLocationCommand ?? (_completeEndLocationCommand = new RelayCommand(OnExecuteCompleteEndLocation));
        private ICommand _completeEndLocationCommand;

        private void OnExecuteCompleteEndLocation(object parameter)
        {
            if (!(parameter is Station station))
                return;

            EndLocation = station.Name;
            StationList = null;
            IsCompleteEndLocationActive = false;
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

        
        /// <summary>
        /// Gets or sets the start location.
        /// </summary>
        public string StartLocation
        {
            get => _startLocation;
            set
            {
                _startLocation = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    GetStations(value);
                    if(StationList.StationList.Any())
                        IsCompleteStartLocationActive = true;
                }
                else
                    IsCompleteStartLocationActive = false;

                RaisePropertyChanged();
            }
        }
        private string _startLocation;

        /// <summary>
        /// Gets or sets the end location.
        /// </summary>
        public string EndLocation
        {
            get => _endLocation;
            set
            {
                _endLocation = value;
                if (!string.IsNullOrWhiteSpace(value))
                {
                    GetStations(value);
                    if(StationList.StationList.Any())
                        IsCompleteEndLocationActive = true;
                }
                else
                    IsCompleteEndLocationActive = false;

                RaisePropertyChanged();
            }
        }
        private string _endLocation;

        /// <summary>
        /// Gets or sets a list of stations for the autocomplete function.
        /// </summary>
        public Stations StationList
        {
            get => _stationList;
            set
            {
                _stationList = value;
                RaisePropertyChanged();
            }
        }
        private Stations _stationList;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is auto complete start-location active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is complete start location active; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleteStartLocationActive
        {
            get => _isCompleteStartLocationActive;
            set
            {
                _isCompleteStartLocationActive = value;
                RaisePropertyChanged();
            }
        }
        private bool _isCompleteStartLocationActive;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is auto complete end-location active.
        /// </summary>
        public bool IsCompleteEndLocationActive
        {
            get => _isCompleteEndLocationActive;
            set
            {
                _isCompleteEndLocationActive = value;
                RaisePropertyChanged();
            }
        }
        private bool _isCompleteEndLocationActive;
        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods

        /// <summary>
        /// Gets the stations.
        /// </summary>
        /// <param name="stationName">Name of the station.</param>
        private void GetStations(string stationName)
        {
            var transport = new Transport();
            StationList = transport.GetStations(stationName);
        }

        #endregion
    }
}
