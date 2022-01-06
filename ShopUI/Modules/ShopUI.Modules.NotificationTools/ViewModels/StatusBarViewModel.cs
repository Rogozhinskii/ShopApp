using EventAggregator.Core;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ShopUI.Core;
using System.Windows;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    /// <summary>
    /// Статус бар приложения
    /// </summary>
    public class StatusBarViewModel:BindableBase
    {        
        private readonly IDialogService _dialogService;
        private readonly IEventAggregator _eventAggregator;

        private Visibility _progressBarVisibility = Visibility.Hidden;

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { SetProperty(ref _progressBarVisibility, value); }
        }


        public StatusBarViewModel(IDialogService dialogService,IEventAggregator eventAggregator)
        {            
            _dialogService = dialogService;
            _eventAggregator = eventAggregator;
            _eventAggregator.GetEvent<OnLongOperationEvent>().Subscribe(OnLongOperation);
        }
        
        private void OnLongOperation(Visibility visibility)
        {
            ProgressBarVisibility = visibility;
        }

        private DelegateCommand _showConnectionInfoDialog;

        public DelegateCommand ShowConnectionInfoDialog =>
           _showConnectionInfoDialog ??= _showConnectionInfoDialog = new(ExecuteShowConnectionInfoDialog);

        void ExecuteShowConnectionInfoDialog()
        {
            _dialogService.ShowDialog(CommonTypesPrism.ConnectionInfoDialog,null,null);
        }



    }
}
