using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using SwissTransport;
using TransportApp.Base;
using TransportApp.Views;

namespace TransportApp.ViewModels
{
    public class StationBoardViewModel : BaseViewModel
    {
        #region Initialization



        #endregion

        #region Commands

        #region Show StationBoard

        public ICommand SearchTrainStationCommand => _searchTrainStationCommand ?? (_searchTrainStationCommand = new RelayCommand(OnExecuteSearchTrainStation));
        private ICommand _searchTrainStationCommand;

        private void OnExecuteSearchTrainStation(object parameter)
        {
            if (string.IsNullOrWhiteSpace(Station))
                return;

            try
            {
                var transport = new Transport();
                var stationId = transport.GetStations(Station).StationList.FirstOrDefault(x => string.Equals(x.Name, Station, StringComparison.CurrentCultureIgnoreCase))?.Id;
                if (stationId == null)
                    throw new Exception("This station does not exist.");
                StationBoards = transport.GetStationBoard(Station, stationId).Entries;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        #endregion

        #region Back to MainPage

        public ICommand BackCommand => _backCommand ?? (_backCommand = new RelayCommand(OnExecuteBack));
        private ICommand _backCommand;

        private void OnExecuteBack(object parameter)
        {
            var transportView = new TransportView();
            RequestClose?.Invoke();
            transportView.ShowDialog();
        }

        #endregion

        #region Complete Location

        public ICommand CompleteStationCommand => _completeStationCommand ?? (_completeStationCommand = new RelayCommand(OnExecuteCompleteEndLocation));
        private ICommand _completeStationCommand;

        private void OnExecuteCompleteEndLocation(object parameter)
        {
            if (!(parameter is Station station))
                return;

            Station = station.Name;
            StationList = null;
            IsCompleteStationActive = false;
        }

        #endregion

        #region Lost Focus on Location

        public ICommand LostFocusOnStationCommand => _lostFocusOnStationCommand ?? (_lostFocusOnStationCommand = new RelayCommand(OnExecuteLostFocusOnStation));
        private ICommand _lostFocusOnStationCommand;

        private void OnExecuteLostFocusOnStation(object parameter)
        {
            if(NewFocusElementIsListViewItem)
                return;

            StationList = null;
            IsCompleteStationActive = false;
        }

        #endregion

        #endregion

        #region Properties

        /// <summary>
        /// Gets or sets the station.
        /// </summary>
        public string Station
        {
            get => _station;
            set
            {
                _station = value;
                AutoCompleteStation(value);
                RaisePropertyChanged();
            }
        }

        private string _station;

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
        /// Gets or sets the station boards.
        /// </summary>
        public List<StationBoard> StationBoards
        {
            get => _stationBoards;
            set
            {
                _stationBoards = value;
                RaisePropertyChanged();
            }
        }
        private List<StationBoard> _stationBoards;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is auto complete station active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is complete start location active; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleteStationActive
        {
            get => _isCompleteStationActive;
            set
            {
                _isCompleteStationActive = value;
                RaisePropertyChanged();
            }
        }
        private bool _isCompleteStationActive;

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
        /// Gets the stations.
        /// </summary>
        /// <param name="stationName">Name of the station.</param>
        private void GetStations(string stationName)
        {
            var transport = new Transport();
            StationList = transport.GetStations(stationName);
        }

        /// <summary>
        /// Automatics the complete station.
        /// </summary>
        /// <param name="value">The value.</param>
        private void AutoCompleteStation(string value)
        {

            if (!string.IsNullOrWhiteSpace(value))
            {
                StationList = null;
                GetStations(value);
                if (StationList.StationList.Any())
                    IsCompleteStationActive = true;
            }
            else
                IsCompleteStationActive = false;
        }

        #endregion
    }
}
