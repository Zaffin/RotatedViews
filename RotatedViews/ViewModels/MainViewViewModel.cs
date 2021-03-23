namespace RotatedViews.ViewModel
{
    using System.Windows;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Runtime.CompilerServices;

    using Mastercam.Database;

    using RotatedViews.Services;
    using RotatedViews.Commands;
    using RotatedViews.DataTypes;

    public class MainViewViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private readonly IMastercamService mastercamService;

        private readonly IViewService viewService;

        private List<MCView> views;

        private MCView selectedView;

        private bool isXAxis;

        private bool isYAxis;

        private bool isZAxis;

        private int numberOfViews;

        private double rotationAngle;

        private bool isAngleBetween;

        private bool isTotalSweep;

        #endregion

        #region Public Properties

        public List<MCView> Views
        {
            get => this.views;
            set
            {
                this.views = value;
                OnPropertyChanged();
            }
        }

        public MCView SelectedView
        {
            get => this.selectedView;
            set
            {
                this.selectedView = value;
                OnPropertyChanged();
            }
        }

        public bool IsXAxis
        {
            get => this.isXAxis;
            set
            {
                this.isXAxis = value;
                OnPropertyChanged();
            }
        }

        public bool IsYAxis
        {
            get => this.isYAxis;
            set
            {
                this.isYAxis = value;
                OnPropertyChanged();
            }
        }

        public bool IsZAxis
        {
            get => this.isZAxis;
            set
            {
                this.isZAxis = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfViews
        {
            get => this.numberOfViews;
            set
            {
                this.numberOfViews = value;
                OnPropertyChanged();
            }
        }

        public double RotationAngle
        {
            get => this.rotationAngle;
            set
            {
                this.rotationAngle = value;
                OnPropertyChanged();
            }
        }

        public bool IsAngleBetween
        {
            get => this.isAngleBetween;
            set
            {
                this.isAngleBetween = value;
                OnPropertyChanged();
            }
        }

        public bool IsTotalSweep
        {
            get => this.isTotalSweep;
            set
            {
                this.isTotalSweep = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        public MainViewViewModel(IMastercamService mastercamService, IViewService viewService)
        {
            this.mastercamService = mastercamService;
            this.viewService = viewService;

            this.ApplyCommand = new DelegateCommand(OnApplyCommand);
            this.OkCommand = new DelegateCommand(OnOkCommand);
            this.CancelCommand = new DelegateCommand(OnCancelCommand);

            this.Views = this.mastercamService.GetViews();
            this.SelectedView = Views.Find(v => v.ViewID == mastercamService.GetCurrentConstructionView().ViewID);

            this.IsZAxis = true;

            this.NumberOfViews = 1;
            this.RotationAngle = 45.0;

            IsAngleBetween = true;
        }

        #endregion

        #region Commands

        public ICommand ApplyCommand { get; }

        public ICommand OkCommand { get; }

        public ICommand CancelCommand { get; }

        #endregion

        #region Private Methods  

        private void OnApplyCommand(object parameter)
        {
            viewService.CreateRotatedViews(SelectedView,
                                           GetRotationAxis(),
                                           NumberOfViews,
                                           RotationAngle,
                                           IsTotalSweep);
        }

        private void OnOkCommand(object parameter)
        {
            var view = (Window)parameter;

            viewService.CreateRotatedViews(SelectedView,
                                           GetRotationAxis(), 
                                           NumberOfViews, 
                                           RotationAngle, 
                                           IsTotalSweep);

            view?.Close();
        }

        private void OnCancelCommand(object parameter)
        {
            var view = (Window)parameter;

            view?.Close();
        }

        private RotationAxis GetRotationAxis()
        {
            if (IsXAxis)
            {
                return new RotationAxis
                {
                    Axis = SelectedView.ViewMatrix.Row1,
                    Label = "X"
                };
            }
            else if (IsYAxis)
            {
                return new RotationAxis
                {
                    Axis = SelectedView.ViewMatrix.Row2,
                    Label = "Y"
                };
            }
            else
            {
                return new RotationAxis
                {
                    Axis = SelectedView.ViewMatrix.Row3,
                    Label = "Z"
                };
            }
               
        }

        #endregion

        #region Notify Property Changed

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}