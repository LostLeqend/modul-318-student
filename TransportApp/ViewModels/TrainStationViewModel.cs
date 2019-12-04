using System;
using System.Windows.Input;
using Microsoft.Maps.MapControl.WPF;
using SwissTransport;
using TransportApp.Base;
using TransportApp.Views;

namespace TransportApp.ViewModels
{
    public class TrainStationViewModel : BaseViewModel
    {
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

        #region Complete Location

        public ICommand CompleteLocationCommand => _completeLocationCommand ?? (_completeLocationCommand = new RelayCommand(OnExecuteCompleteLocation));
        private ICommand _completeLocationCommand;

        private void OnExecuteCompleteLocation(object parameter)
        {
            if (!(parameter is Station station))
                return;

            Station = station.Name;
            StationList = null;
            IsCompleteLocationActive = false;
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
            IsCompleteLocationActive = false;
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

        /// <summary>
        /// Gets or sets a value indicating whether this instance is auto complete start-location active.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is complete start location active; otherwise, <c>false</c>.
        /// </value>
        public bool IsCompleteLocationActive
        {
            get => _isCompleteLocationActive;
            set
            {
                _isCompleteLocationActive = value;
                RaisePropertyChanged();
            }
        }
        private bool _isCompleteLocationActive;

        /// <summary>
        /// True if new focus element is ListViewItem.
        /// </summary>
        public static bool NewFocusElementIsListViewItem { get; set; }
        #endregion

        #region Events

        public event Action RequestClose;

        #endregion

        #region Methods



        #endregion
    }
}
