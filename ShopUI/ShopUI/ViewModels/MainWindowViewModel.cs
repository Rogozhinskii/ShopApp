using Prism.Mvvm;
using Prism.Regions;
using ShopUI.Core;

namespace ShopUI.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        private readonly IRegionManager _regionManager;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        private bool _isProductsTabSelected;
        

        public bool IsProductsTabSelected
        {
            get { return _isProductsTabSelected; }
            set 
            { 
                SetProperty(ref _isProductsTabSelected, value);
                if (value)
                    _regionManager.RequestNavigate(RegionNames.ProductsRegion,ViewNames.ProductsView);

            }
        }


        public MainWindowViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }
    }
}
