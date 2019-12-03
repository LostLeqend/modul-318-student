using System;
using System.Windows.Input;
using SwissTransport;
using TransportApp.Base;
using TransportApp.Views;

namespace TransportApp.ViewModels
{
    public class StationBoardViewModel
    {
        #region Initialization



        #endregion

        #region Commands

        #region Show StationBoard

        public ICommand SearchTrainStationCommand => _searchTrainStationCommand ?? (_searchTrainStationCommand = new RelayCommand(OnExecuteSearchTrainStation));
        private ICommand _searchTrainStationCommand;

        private void OnExecuteSearchTrainStation(object parameter)
        {
            var transport = new Transport();
            Station = transport.GetStations("luzern")
            var stationBoard = transport.GetStationBoard(testStation, "0");
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

        public Station Station { get; set; }

        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods



        #endregion
    }
}
