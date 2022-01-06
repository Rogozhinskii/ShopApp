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

        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isSignedIn;

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


        private bool _isProductsTabSelected;
        public bool IsProductsTabSelected
        {
            get { return _isProductsTabSelected; }
            set {SetProperty(ref _isProductsTabSelected, value);}
        }

         

        private DelegateCommand _startApplicationCommand;

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




        public MainWindowViewModel(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }
    }
}
