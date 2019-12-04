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
                RaisePropertyChanged();
            }
        }

        private string _station;

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

        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods



        #endregion
    }
}
