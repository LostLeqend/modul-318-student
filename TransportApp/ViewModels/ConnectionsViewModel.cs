using System;
using System.Collections.Generic;
using System.Windows.Input;
using SwissTransport;
using TransportApp.Base;
using TransportApp.Views;

namespace TransportApp.ViewModels
{
    public class ConnectionsViewModel : BaseViewModel
    {
        #region Initialization



        #endregion

        #region Commands

        #region Search connection

        public ICommand SearchConnectionCommand => _searchConnectionCommand ?? (_searchConnectionCommand = new RelayCommand(OnExecuteSearchConnection));
        private ICommand _searchConnectionCommand;

        private void OnExecuteSearchConnection()
        {
            if(StartLocation == string.Empty || EndLocation == string.Empty)
                return;

            var transport = new Transport();
            ConnectionList = transport.GetConnections(StartLocation, EndLocation);
        }

        #endregion

        #region Back to MainPage

        public ICommand BackCommand => _backCommand ?? (_backCommand = new RelayCommand(OnExecuteBack));
        private ICommand _backCommand;

        private void OnExecuteBack()
        {
            var transportView = new TransportView();
            RequestClose?.Invoke();
            transportView.ShowDialog();
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
        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods



        #endregion
    }
}
