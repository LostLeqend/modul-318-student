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

        private void OnExecuteSerchTrainStation()
        {
            var testStation = "Luzern";
            var transport = new Transport();
            var stationBoard = transport.GetStationBoard(testStation, "0");
        }

        #endregion

        #endregion

        #region Properties



        #endregion

        #region Methods



        #endregion
    }
}
