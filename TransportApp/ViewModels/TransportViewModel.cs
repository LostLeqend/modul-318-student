using System;
using System.Linq;
using System.Windows;
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

        public ICommand ShowConnectionsCommand => _showConnectionsCommand ?? (_showConnectionsCommand = new RelayCommand(OnExecuteShowConnections));
        private ICommand _showConnectionsCommand;

        private void OnExecuteShowConnections(object parameter)
        {
            var connectionsView = new ConnectionsView();
            RequestClose?.Invoke();
            connectionsView.ShowDialog();
        }

        #endregion

        #region Show station board

        public ICommand ShowStationBoardCommand => _showStationBoardCommand ?? (_showStationBoardCommand = new RelayCommand(OnExecuteShowStationBoard));
        private ICommand _showStationBoardCommand;

        private void OnExecuteShowStationBoard(object parameter)
        {
            var stationBoardView = new StationBoardView();
            RequestClose?.Invoke();
            stationBoardView.ShowDialog();
        }

        #endregion

        #region Show train station

        public ICommand ShowTrainStation => _showTrainStation ?? (_showTrainStation = new RelayCommand(OnExecuteShowTrainStation));
        private ICommand _showTrainStation;

        private void OnExecuteShowTrainStation(object parameter)
        {
            var trainStationView = new TrainStationView();
            RequestClose?.Invoke();
            trainStationView.ShowDialog();
        }

        #endregion

        #region Complete StartLocation

        public ICommand QuickConnectionSearchCommand => _quickConnectionSearchCommand ?? (_quickConnectionSearchCommand = new RelayCommand(OnExecuteQuickConnectionSearch));
        private ICommand _quickConnectionSearchCommand;

        private void OnExecuteQuickConnectionSearch(object parameter)
        {
            if (!IsStationValid())
                return;

            try
            {
                var date = DateTime.Now.ToShortDateString();
                var time = DateTime.Now.ToShortTimeString();

                var transport = new Transport();
                ConnectionList = transport.GetConnections(StartLocation, EndLocation, date, time, false);

                //Format datetime manual, because it's from the type string
                foreach (var connection in ConnectionList.ConnectionList)
                {
                    connection.Duration = connection.Duration.Remove(0, 3);
                    connection.Duration = connection.Duration.Remove(connection.Duration.Length - 3, 3);

                    if (connection.Duration.StartsWith("00:"))
                    {
                        connection.Duration = connection.Duration.Remove(0, 3);
                        connection.Duration += " min";
                    }
                    else
                    {
                        connection.Duration += " h";
                    }

                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
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

        #region Lost Focus on Location

        public ICommand LostFocusOnLocationCommand => _lostFocusOnLocationCommand ?? (_lostFocusOnLocationCommand = new RelayCommand(OnExecuteLostFocusOnLocation));
        private ICommand _lostFocusOnLocationCommand;

        private void OnExecuteLostFocusOnLocation(object parameter)
        {
            if(NewFocusElementIsListViewItem)
                return;

            StationList = null;
            IsCompleteStartLocationActive = false;
            IsCompleteEndLocationActive = false;
        }

        #endregion

        #endregion

        #region Properties


        /// <summary>
        /// Gets or sets the start location.
        /// </summary>
        public string StartLocation
        {
            get => _startLocation;
            set
            {
                _startLocation = value;
                AutoCompleteStartLocation(value);
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
                AutoCompleteEndLocation(value);
                RaisePropertyChanged();
            }
        }
        private string _endLocation;

        /// <summary>
        /// Gets or sets the connections.
        /// </summary>
        public Connections ConnectionList
        {
            get => _connectionList;
            set
            {
                _connectionList = value;
                RaisePropertyChanged();
            }
        }
        private Connections _connectionList;

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

        /// <summary>
        /// True if new focus element is ListViewItem.
        /// </summary>
        public static bool NewFocusElementIsListViewItem { get; set; }
        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether [is station valid].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is station valid]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsStationValid()
        {
            if (string.IsNullOrWhiteSpace(StartLocation) || string.IsNullOrWhiteSpace(EndLocation))
                return false;
            return true;
        }

        /// <summary>
        /// Gets the stations.
        /// </summary>
        /// <param name="stationName">Name of the station.</param>
        private void GetStations(string stationName)
        {
            var transport = new Transport();
            StationList = transport.GetStations(stationName);
        }

        /// <summary>
        /// Automatics the complete start location.
        /// </summary>
        /// <param name="value">The value.</param>
        private void AutoCompleteStartLocation(string value)
        {

            if (!string.IsNullOrWhiteSpace(value))
            {
                StationList = null;
                GetStations(value);
                if (StationList.StationList.Any())
                    IsCompleteStartLocationActive = true;
                IsCompleteEndLocationActive = false;
            }
            else
                IsCompleteStartLocationActive = false;
        }

        /// <summary>
        /// Automatics the complete end location.
        /// </summary>
        /// <param name="value">The value.</param>
        private void AutoCompleteEndLocation(string value)
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                StationList = null;
                GetStations(value);
                if (StationList.StationList.Any())
                    IsCompleteEndLocationActive = true;
                IsCompleteStartLocationActive = false;
            }
            else
                IsCompleteEndLocationActive = false;
        }
        #endregion
    }
}
