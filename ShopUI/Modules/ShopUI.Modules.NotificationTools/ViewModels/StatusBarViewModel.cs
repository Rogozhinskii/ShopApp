using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using ShopUI.Services.Interfaces;
using System.Windows;

namespace ShopUI.Modules.NotificationTools.ViewModels
{
    public class StatusBarViewModel:BindableBase
    {
        
        private readonly IDialogService _dialogService;

        public StatusBarViewModel(IDialogService dialogService)
        {            
            _dialogService = dialogService;
        }
        private Visibility _progressBarVisibility;
        

        public Visibility ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { SetProperty(ref _progressBarVisibility, value); }
        }

        private DelegateCommand _showConnectionInfoDialog;

        public DelegateCommand ShowConnectionInfoDialog =>
           _showConnectionInfoDialog ??= _showConnectionInfoDialog = new(ExecuteShowConnectionInfoDialog);

        void ExecuteShowConnectionInfoDialog()
        {
            _dialogService.ShowDialog("ConnectionInfoDialog",null,null);
        }



    }
}
