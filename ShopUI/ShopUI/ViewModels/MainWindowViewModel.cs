using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using Prism.Services.Dialogs;
using ShopUI.Core;
using System.Windows;

namespace ShopUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Shop App";
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;


        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isSignedIn;
        /// <summary>
        /// Флаг был ли выполнен вход в приложение
        /// </summary>
        public bool IsSignedIn
        {
            get { return _isSignedIn; }
            set 
            { 
                SetProperty(ref _isSignedIn, value); 
                if (value) 
                    _regionManager.RequestNavigate(RegionNames.CustomersRegion, CommonTypesPrism.CustomersView); 
            }
        }

        private DelegateCommand _startApplicationCommand;

        /// <summary>
        /// Вызывает окно входа в приложение при запуске. При неудачной попытке закрывает приложение
        /// </summary>
        public DelegateCommand StartApplicationCommand =>
           _startApplicationCommand ??= _startApplicationCommand = new(ExecuteStartApplicationCommand);

        void ExecuteStartApplicationCommand()
        {
            _dialogService.ShowDialog(CommonTypesPrism.AuthenticationDialog, null, (result) =>
             {
                 IsSignedIn = result.Parameters.GetValue<bool>(CommonTypesPrism.logInResult);                
                 if (IsSignedIn == false || result.Result!=ButtonResult.OK)
                 {
                     Application.Current.Shutdown();
                 }
             });
        }

    }
}
