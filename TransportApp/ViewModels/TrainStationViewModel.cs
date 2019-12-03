using System;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
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

        public Location Location
        {
            get => _location;
            set
            {
                _location = value;
                RaisePropertyChanged();
            }
        }
        private Location _location = new Location(46, 8);

        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods



        #endregion
    }
}
