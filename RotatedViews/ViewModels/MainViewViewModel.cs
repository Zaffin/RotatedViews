namespace RotatedViews.ViewModel
{
    using System;
    using System.Windows;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Input;
    using System.Runtime.CompilerServices;

    using Mastercam.Database;

    using RotatedViews.Services;
    using RotatedViews.Commands;
    using RotatedViews.Models;


    public class MainViewViewModel : INotifyPropertyChanged
    {
        #region Private Fields

        private readonly IMastercamService mastercamService;

        private readonly IViewService viewService;

        private readonly ISettingsService settingsService;

        private List<MCView> views;

        private MCView selectedView;

        private ViewAxis selectedViewAxis;

        private int numberOfViews;

        private double rotationAngle;

        private DistanceType selectedDistanceType;

        private string viewNameTemplate;

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

        public ViewAxis SelectedViewAxis
        {
            get => this.selectedViewAxis;
            set
            {
                this.selectedViewAxis = value;
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

        public DistanceType SelectedDistanceType
        {
            get => this.selectedDistanceType;
            set
            {
                this.selectedDistanceType = value;
                OnPropertyChanged();
            }
        }

        public string ViewNameTemplate
        {
            get => this.viewNameTemplate;
            set
            {
                this.viewNameTemplate = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Construction

        public MainViewViewModel(IMastercamService mastercamService, IViewService viewService, ISettingsService settingsService)
        {
            this.mastercamService = mastercamService;
            this.viewService = viewService;
            this.settingsService = settingsService;

            this.ApplyCommand = new DelegateCommand(OnApplyCommand);
            this.OkCommand = new DelegateCommand(OnOkCommand);
            this.CancelCommand = new DelegateCommand(OnCancelCommand);
            this.SaveCommand = new DelegateCommand(OnSaveCommand);
            this.ResetCommand = new DelegateCommand(OnResetCommand);

            RefreshViewList(true);

            this.SelectedViewAxis = ViewAxis.ZAxis;

            this.NumberOfViews = 1;
            this.RotationAngle = 45.0;

            this.SelectedDistanceType = DistanceType.AngleBetween;

            this.ViewNameTemplate = settingsService.GetDefaultViewNameTemplate();
        }

        #endregion

        #region Commands

        public ICommand ApplyCommand { get; }

        public ICommand OkCommand { get; }

        public ICommand CancelCommand { get; }

        public ICommand SaveCommand { get; }

        public ICommand ResetCommand { get; }

        #endregion

        #region Private Methods  

        private void OnApplyCommand(object parameter)
        {
            viewService.CreateRotatedViews(SelectedView,
                                           SelectedViewAxis,
                                           NumberOfViews,
                                           RotationAngle,
                                           SelectedDistanceType,
                                           ViewNameTemplate);

            RefreshViewList();
        }

        private void OnOkCommand(object parameter)
        {
            var view = (Window)parameter;

            viewService.CreateRotatedViews(SelectedView,
                                           SelectedViewAxis, 
                                           NumberOfViews, 
                                           RotationAngle,
                                           SelectedDistanceType,
                                           ViewNameTemplate);

            view?.Close();
        }

        private void OnCancelCommand(object parameter)
        {
            var view = (Window)parameter;

            view?.Close();
        }

        private void OnSaveCommand(object parameter)
        {
            settingsService.SaveViewNameTemplateAsDefault(ViewNameTemplate);
        }

        private void OnResetCommand(object parameter)
        {
            ViewNameTemplate = settingsService.GetDefaultViewNameTemplate();
        }

        private void RefreshViewList(bool isUsingConstructionView = false)
        {
            var selectedViewID = SelectedView?.ViewID;

            if (isUsingConstructionView)
            {
                selectedViewID = mastercamService.GetCurrentConstructionView().ViewID;
            }
            
            this.Views = this.mastercamService.GetViews();
            this.SelectedView = Views.Find(v => v.ViewID == selectedViewID);
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