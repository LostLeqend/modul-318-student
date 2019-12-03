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

        #region Show connections

        public ICommand SearchTrainStationCommand => _searchTrainStationCommand ?? (_searchTrainStationCommand = new RelayCommand(OnExecuteSerchTrainStation));
        private ICommand _searchTrainStationCommand;

        private void OnExecuteSerchTrainStation(object parameter)
        {
            var testStation = "Luzern";
            var transport = new Transport();
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



        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods



        #endregion
    }
}
