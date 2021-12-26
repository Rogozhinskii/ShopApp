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
        private string _title = "Prism Application";
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
            set { SetProperty(ref _isSignedIn, value); }
        }


        private bool _isProductsTabSelected;
        public bool IsProductsTabSelected
        {
            get { return _isProductsTabSelected; }
            set 
            { 
                SetProperty(ref _isProductsTabSelected, value);
                if (value && IsSignedIn)
                    _regionManager.RequestNavigate(RegionNames.ProductsRegion,CommonTypesPrism.ProductsView);

            }
        }

        private DelegateCommand _startApplicationCommand;

        public DelegateCommand StartApplicationCommand =>
           _startApplicationCommand ??= _startApplicationCommand = new DelegateCommand(ExecuteStartApplicationCommand);

        void ExecuteStartApplicationCommand()
        {
            _dialogService.ShowDialog(CommonTypesPrism.AuthenticationDialog, null, (result) =>
             {
                 var res=result.Parameters.GetValue<bool>(CommonTypesPrism.logInResult);
                 if (res == false)
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
